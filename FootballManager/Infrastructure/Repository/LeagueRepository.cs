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
                .Include(l => l.CurrentSeason)
                .Take(100).ToListAsync();
        }

        public async Task<League> GetLeagueById(long id)
        {
            return await _context.Leagues
                .Include(l => l.Teams).ThenInclude(t => t.CurrentTeamSheet)
                .Include(l => l.Teams).ThenInclude(t => t.CurrentSeasonStats)
                .Include(l => l.Teams).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerPerson)
                .Include(l => l.Teams).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(l => l.Teams).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerStats)
                .Include(l => l.Teams).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(l => l.Fixtures)
                .Include(l => l.CurrentSeason)
                .SingleOrDefaultAsync(l => l.LeagueId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLeague(League u)
        {
            _context.Leagues.Attach(u);
        }
    }
}
