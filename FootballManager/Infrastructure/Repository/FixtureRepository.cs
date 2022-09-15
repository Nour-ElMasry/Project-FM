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

        public async Task Clear()
        {
            await Task.Run(() => _context.Fixtures.RemoveRange(_context.Fixtures.ToList()));
        }

        public async Task DeleteFixture(Fixture u)
        {
            await Task.Run(() => _context.Fixtures.Remove(u));
        }

        public async Task<List<Fixture>> GetAllFixtures()
        {
            return await _context.Fixtures
                .Include(l => l.HomeTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(l => l.AwayTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(l => l.HomeTeam).ThenInclude(t => t.CurrentSeasonStats)
                .Include(l => l.AwayTeam).ThenInclude(t => t.CurrentSeasonStats)
                .Include(l => l.HomeTeam).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(l => l.AwayTeam).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(f => f.FixtureLeague)
                .ToListAsync();
        }

        public async Task<Fixture> GetFixtureById(long id)
        {
            return await _context.Fixtures
                .Include(l => l.HomeTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(l => l.AwayTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(l => l.HomeTeam).ThenInclude(t => t.CurrentSeasonStats)
                .Include(l => l.AwayTeam).ThenInclude(t => t.CurrentSeasonStats)
                .Include(l => l.HomeTeam).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(l => l.AwayTeam).ThenInclude(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(f => f.FixtureLeague)
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
