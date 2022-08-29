using Application.Abstract;
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

        public async Task<List<League>> GetAllLeagues()
        {
            return await _context.Leagues
                .Take(100).ToListAsync();
        }

        public async Task<League> GetLeagueById(long id)
        {
            return await _context.Leagues
                .Include(l => l.Teams)
                .Include(l => l.Teams).ThenInclude(t => t.CurrentTeamSheet)
                .Include(l => l.Teams).ThenInclude(t => t.CurrentSeasonStats)
                .Include(l => l.Fixtures)
                .SingleOrDefaultAsync(l => l.LeagueId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLeague(long id, League u)
        {
            var league = await GetLeagueById(id);

            if (league != null)
            {
                league.Name = u.Name;
            }
        }
    }
}
