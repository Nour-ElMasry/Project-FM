using Application.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class FixtureRepository : IFixtureRepository
    {
        private readonly DataContext _context;

        public FixtureRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task AddFixture(Fixture u)
        {
            await _context.Fixtures.AddAsync(u);
        }

        public async Task ClearLeagueFixtures(long leagueId)
        {
            await Task.Run(() => _context.Fixtures.RemoveRange(_context.Fixtures.Where(f => f.FixtureLeague.LeagueId == leagueId).ToList()));
        }

        public async Task DeleteFixture(Fixture u)
        {
            await Task.Run(() => _context.Fixtures.Remove(u));
        }

        public async Task<List<Fixture>> GetAllFixtures()
        {
            return await _context.Fixtures
                .Include(f => f.HomeTeam)
                .Include(f => f.AwayTeam)
                .Include(f => f.FixtureLeague)
                .Include(f => f.FixtureScore)
                .ToListAsync();
        }

        public async Task<List<Fixture>> GetAllFixturesByLeague(long leagueId)
        {
            return await _context.Fixtures.Where(f => f.FixtureLeague.LeagueId == leagueId)
                 .Include(f => f.HomeTeam)
                 .Include(f => f.AwayTeam)
                 .Include(f => f.FixtureLeague)
                 .Include(f => f.FixtureScore)
                 .ToListAsync();
        }

        public async Task<List<Fixture>> GetAllFixturesForSimulation()
        {
            return await _context.Fixtures
                .Include(f => f.HomeTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(f => f.AwayTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(f => f.HomeTeam).ThenInclude(t => t.CurrentSeasonStats)
                .Include(f => f.AwayTeam).ThenInclude(t => t.CurrentSeasonStats)
                .Include(f => f.HomeTeam).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(f => f.AwayTeam).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(f => f.FixtureLeague)
                .Include(f => f.FixtureScore)
                .ToListAsync();
        }

        public async Task<Fixture> GetFixtureById(long id)
        {
            return await _context.Fixtures
                .Include(f => f.HomeTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(f => f.AwayTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(f => f.HomeTeam).ThenInclude(t => t.CurrentSeasonStats)
                .Include(f => f.AwayTeam).ThenInclude(t => t.CurrentSeasonStats)
                .Include(f => f.HomeTeam).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(f => f.AwayTeam).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(f => f.FixtureEvents).ThenInclude(f => f.GoalScorer.PlayerPerson)
                .Include(f => f.FixtureEvents).ThenInclude(f => f.GoalAssister.PlayerPerson)
                .Include(f => f.FixtureLeague)
                .Include(f => f.FixtureScore)
                .SingleOrDefaultAsync(f => f.FixtureId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateFixture(Fixture u)
        {
            return Task.Run(() => _context.Fixtures.Attach(u));
        }
    }
}
