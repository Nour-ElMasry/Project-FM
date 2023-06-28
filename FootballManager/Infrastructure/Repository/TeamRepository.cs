using Application.Abstract;
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

        public async Task<Pager<Team>> GetAllTeams(int pg)
        {
            var page = new Pager<Team>(await _context.Teams.CountAsync(), pg);

            if (pg == 0)
            {
                page.PageResults = await _context.Teams
                .Include(t => t.CurrentSeasonStats)
                .ToListAsync();

                return page;
            }

            page.PageResults = await _context.Teams
                .Include(t => t.CurrentSeasonStats)
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
                .Include(t => t.CurrentTeamSheet).ThenInclude(t => t.TeamTactic)
                .Include(t => t.CurrentLeague)
                .SingleOrDefaultAsync(t => t.TeamId == id);
        }

        public async Task<List<Team>> GetTeamsByLeagueId(long leagueId)
        {
            return await _context.Teams.Where(t => t.CurrentLeague.LeagueId == leagueId)
            .Include(t => t.CurrentSeasonStats)
            .OrderByDescending(t => t.CurrentSeasonStats.Points).ThenByDescending(t => t.CurrentSeasonStats.GoalsFor - t.CurrentSeasonStats.GoalsAgainst).ThenBy(t => t.Name)
            .ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateTeam(Team u)
        {
            return Task.Run(() => _context.Teams.Attach(u));
        }

        public async Task<List<Team>> GetTeamsNotAssignedToLeagues()
        {
            return await _context.Teams.Include(t => t.CurrentTeamSheet).Where(t => t.CurrentLeague == null).ToListAsync();
        }

        public async Task<Team> GetTeamWithTactics(long id)
        {
            return await _context.Teams
                .Include(t => t.Players).ThenInclude(p => p.CurrentPlayerStats)
                .Include(t => t.CurrentTeamSheet).ThenInclude(ts => ts.TeamTactic)
                .SingleOrDefaultAsync(t => t.TeamId == id);
        }

        public async Task<Team> GetTeamLineup(long id)
        {
            return await _context.Teams
                .Include(t => t.CurrentTeamSheet).ThenInclude(ts => ts.TeamTactic)
                .Include(t => t.CurrentTeamSheet).ThenInclude(ts => ts.StartingEleven).ThenInclude(p => p.PlayerPerson)
                .Include(t => t.CurrentTeamSheet).ThenInclude(ts => ts.StartingEleven).ThenInclude(p => p.CurrentPlayerStats)
                .Include(t => t.CurrentTeamSheet).ThenInclude(ts => ts.Bench).ThenInclude(p => p.PlayerPerson)
                .Include(t => t.CurrentTeamSheet).ThenInclude(ts => ts.Bench).ThenInclude(p => p.CurrentPlayerStats)
                .Include(t => t.CurrentTeamSheet).ThenInclude(ts => ts.TeamFormation)
                .SingleOrDefaultAsync(t => t.TeamId == id);
        }
    }
}
