using Application.Abstract;
using Application.Pagination;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly DataContext _context;

        public LeagueRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task AddLeague(League u)
        {
            await _context.Leagues.AddAsync(u);
        }

        public async Task DeleteLeague(League u)
        {
            await Task.Run(() => _context.Leagues.Remove(u));
        }

        public async Task<Pager<League>> GetAllLeagues(int pg)
        {
            var page = new Pager<League>(await _context.Leagues.CountAsync(), pg);

            if (pg == 0)
            {
                page.PageResults = await _context.Leagues
               .Include(l => l.CurrentSeason)
               .ToListAsync();

                return page;
            }

            page.PageResults = await _context.Leagues
                .Include(l => l.CurrentSeason)
                .Skip((pg - 1) * 10)
                .Take(10)
                .ToListAsync();

            return page;
        }

        public async Task<List<League>> GetAllLeaguesWithTeamsAndPlayers()
        {
            return await _context.Leagues
               .Include(l => l.CurrentSeason)
               .Include(l => l.Teams).ThenInclude(t => t.Players)
               .ToListAsync();
        }

        public async Task<List<League>> GetAllLeaguesWithTeamsForCampain()
        {
            return await _context.Leagues
              .Include(l => l.Teams)
              .ToListAsync();
        }

        public async Task<League> GetLeagueById(long id)
        {
            return await _context.Leagues
                .Include(l => l.CurrentSeason)
                .SingleOrDefaultAsync(l => l.LeagueId == id);
        }

        public async Task<League> GetLeagueWithTeamsAndPlayersById(long leagueId)
        {
            return await _context.Leagues
               .Include(l => l.CurrentSeason)
               .Include(l => l.Teams).ThenInclude(t => t.Players)
               .SingleOrDefaultAsync(l => l.LeagueId == leagueId);
        }

        public async Task<League> GetLeagueWithTeamsById(long leagueId)
        {
            return await _context.Leagues
               .Include(l => l.CurrentSeason)
               .Include(l => l.Teams)
               .SingleOrDefaultAsync(l => l.LeagueId == leagueId);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateLeague(League u)
        {
            return Task.Run(() => _context.Leagues.Attach(u));
        }
    }
}
