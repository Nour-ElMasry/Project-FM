using Application.Abstract;
using Application.Filters;
using Application.Pagination;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext _context;

        public TeamRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task AddTeam(Team u)
        {
            await _context.Teams.AddAsync(u);
        }

        public async Task DeleteTeam(Team u)
        {
            await Task.Run(() => _context.Teams.Remove(u));
        }

        public async Task<Pager<Team>> GetAllTeams(int pg, TeamFilter filter)
        {
            var page = new Pager<Team>(await _context.Teams.CountAsync(), pg);

            if (pg == 0)
            {
                page.PageResults = await _context.Teams
                .Where(t => ApplyTeamFilter(filter, t))
                .Include(t => t.TeamManager).ThenInclude(tm => tm.ManagerPerson)
                .Include(t => t.CurrentLeague)
                .Include(t => t.CurrentSeasonStats)
                .Include(t => t.CurrentTeamSheet)
                .ToListAsync();

                return page;
            }

            page.PageResults = await _context.Teams
                .Where(t => ApplyTeamFilter(filter, t))
                .Include(t => t.TeamManager).ThenInclude(tm => tm.ManagerPerson)
                .Include(t => t.CurrentLeague)
                .Include(t => t.CurrentSeasonStats)
                .Include(t => t.CurrentTeamSheet)
                .Skip((pg - 1) * 10)
                .Take(10)
                .ToListAsync();

            return page;
        }

        public async Task<List<Team>> GetTeamsList()
        {
            return await _context.Teams.OrderBy(t => t.Name).ToListAsync();
        }

        public async Task<Team> GetTeamById(long id)
        {
            return await _context.Teams
                .Include(t => t.Players).ThenInclude(p => p.PlayerPerson)
                .Include(t => t.HomeFixtures).ThenInclude(hf => hf.AwayTeam)
                .Include(t => t.AwayFixtures).ThenInclude(af => af.HomeTeam)
                .Include(t => t.CurrentTeamSheet)
                .SingleOrDefaultAsync(t => t.TeamId == id);
        }

        public async Task<Pager<Team>> GetTeamsByLeagueId(long leagueId, int pg)
        {
            var page = new Pager<Team>(await _context.Teams.Where(t => t.CurrentLeague.LeagueId == leagueId).CountAsync(), pg);

            if (pg == 0)
            {
                page.PageResults = await _context.Teams.Where(t => t.CurrentLeague.LeagueId == leagueId)
                .Include(t => t.TeamManager).ThenInclude(tm => tm.ManagerPerson)
                .Include(t => t.CurrentLeague)
                .Include(t => t.CurrentSeasonStats)
                .Include(t => t.CurrentTeamSheet)
                .ToListAsync();

                return page;
            }

            page.PageResults = await _context.Teams.Where(t => t.CurrentLeague.LeagueId == leagueId)
                .Include(t => t.TeamManager).ThenInclude(tm => tm.ManagerPerson)
                .Include(t => t.CurrentLeague)
                .Include(t => t.CurrentSeasonStats)
                .Include(t => t.CurrentTeamSheet)
                .Skip((pg - 1) * 10)
                .Take(10)
                .ToListAsync();

            return page;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateTeam(Team u)
        {
            return Task.Run(() => _context.Teams.Attach(u));
        }

        private static bool ApplyTeamFilter(TeamFilter filter, Team t)
        {

            if (filter.LeagueId == 0 &&
                String.IsNullOrWhiteSpace(filter.Name) &&
                String.IsNullOrWhiteSpace(filter.Country))
                return true;

            if (filter.LeagueId != 0)
                if(!(t.CurrentLeague.LeagueId == filter.LeagueId))
                    return false;

            if (!String.IsNullOrWhiteSpace(filter.Name))
              if(!t.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (!String.IsNullOrWhiteSpace(filter.Country))
                if (!(t.Country.ToLower() == filter.Country.ToLower()))
                    return false;

            return true;
        }
    }
}
