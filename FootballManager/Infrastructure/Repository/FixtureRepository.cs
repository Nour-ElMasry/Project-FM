using Application.Abstract;
using Application.Pagination;
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

        public async Task ClearFixtures()
        {
            await Task.Run(() => _context.Fixtures.RemoveRange(_context.Fixtures.ToList()));
        }

        public async Task ClearLeagueFixtures(long leagueId)
        {
            await Task.Run(() => _context.Fixtures.RemoveRange(_context.Fixtures.Where(f => f.FixtureLeague.LeagueId == leagueId).ToList()));
        }

        public async Task DeleteFixture(Fixture u)
        {
            await Task.Run(() => _context.Fixtures.Remove(u));
        }

        public async Task<bool> EndOfSeasonCheck()
        {
            var fixturesPlayedCount = await _context.Fixtures.Where(f => f.isPlayed).CountAsync();
            var fixturesNotPlayedCount = await _context.Fixtures.Where(f => !f.isPlayed).CountAsync();

            return (fixturesPlayedCount > 0 && fixturesNotPlayedCount == 0);
        }

        public async Task<Pager<Fixture>> GetAllFixtures(int pg)
        {
            var totalResults = await _context.Fixtures.CountAsync();
            var totalResultsPerPage = await _context.Teams.CountAsync() / 2;
            var page = new Pager<Fixture>(totalResults, pg, totalResultsPerPage);

            if (pg == 0)
            {
                page.PageResults = await _context.Fixtures
                .Include(f => f.HomeTeam)
                .Include(f => f.AwayTeam)
                .Include(f => f.FixtureLeague)
                .Include(f => f.FixtureScore)
                .OrderBy(f => f.Date).ThenBy(f => f.FixtureLeague.LeagueId)
                .ToListAsync();

                return page;
            }

            page.PageResults = await _context.Fixtures
                .Include(f => f.HomeTeam)
                .Include(f => f.AwayTeam)
                .Include(f => f.FixtureLeague)
                .Include(f => f.FixtureScore)
                .OrderBy(f => f.Date).ThenBy(f => f.FixtureLeague.LeagueId)
                .Skip((pg - 1) * totalResultsPerPage)
                .Take(totalResultsPerPage)
                .ToListAsync();

            return page;
        }

        public async Task<Pager<Fixture>> GetAllFixturesByLeague(long leagueId, int pg)
        {
            var page = new Pager<Fixture>(await _context.Fixtures.Where(f => f.FixtureLeague.LeagueId == leagueId).CountAsync(), pg);

            if (pg == 0)
            {
                page.PageResults = await _context.Fixtures.Where(f => f.FixtureLeague.LeagueId == leagueId)
                 .Include(f => f.HomeTeam)
                 .Include(f => f.AwayTeam)
                 .Include(f => f.FixtureLeague)
                 .Include(f => f.FixtureScore)
                 .OrderBy(f => f.Date)
                 .ToListAsync();

                return page;
            }

            page.PageResults = await _context.Fixtures.Where(f => f.FixtureLeague.LeagueId == leagueId)
                 .Include(f => f.HomeTeam)
                 .Include(f => f.AwayTeam)
                 .Include(f => f.FixtureLeague)
                 .Include(f => f.FixtureScore)
                 .Skip((pg - 1) * 10)
                 .Take(10)
                 .OrderBy(f => f.Date)
                 .ToListAsync();

            return page;
        }

        public async Task<List<Fixture>> GetAllFixturesByTeam(long teamId)
        {
            return await _context.Fixtures.Where(f => f.HomeTeam.TeamId == teamId || f.AwayTeam.TeamId == teamId)
                 .Include(f => f.HomeTeam)
                 .Include(f => f.AwayTeam)
                 .Include(f => f.FixtureLeague)
                 .Include(f => f.FixtureScore)
                 .OrderBy(f => f.Date)
                 .ToListAsync(); ;
        }

        public async Task<List<Fixture>> GetAllFixturesForSimulation()
        {
            return await _context.Fixtures
                .Include(f => f.HomeTeam).ThenInclude(t => t.CurrentTeamSheet).ThenInclude(t => t.StartingEleven).ThenInclude(p => p.PlayerRecord)
                .Include(f => f.AwayTeam).ThenInclude(t => t.CurrentTeamSheet).ThenInclude(t => t.StartingEleven).ThenInclude(p => p.PlayerRecord)
                .Include(f => f.HomeTeam).ThenInclude(t => t.CurrentSeasonStats)
                .Include(f => f.AwayTeam).ThenInclude(t => t.CurrentSeasonStats)
                .Include(f => f.FixtureLeague)
                .Include(f => f.FixtureScore)
                .ToListAsync();
        }

        public async Task<List<Fixture>> GetAllFixturesForSimulationByGameWeek()
        {
            var totalGameweekFixtures = await _context.Teams.CountAsync() / 2;

            var fixtures = await _context.Fixtures
                .Where(f => !f.isPlayed)
                .Include(f => f.HomeTeam)
                    .ThenInclude(t => t.CurrentSeasonStats)
                .Include(f => f.AwayTeam)
                    .ThenInclude(t => t.CurrentSeasonStats)
                .Include(f => f.HomeTeam)
                    .ThenInclude(t => t.CurrentTeamSheet)
                        .ThenInclude(t => t.StartingEleven)
                            .ThenInclude(p => p.PlayerRecord)
                .Include(f => f.AwayTeam)
                    .ThenInclude(t => t.CurrentTeamSheet)
                        .ThenInclude(t => t.StartingEleven)
                            .ThenInclude(p => p.PlayerRecord)
                .Include(f => f.FixtureLeague)
                .Include(f => f.FixtureScore)
                .OrderBy(f => f.Date)
                .Take(totalGameweekFixtures)
                .ToListAsync();

            return fixtures;

        }

        public async Task<Fixture> GetFixtureById(long id)
        {
            return await _context.Fixtures
                .Include(f => f.HomeTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(f => f.AwayTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(f => f.FixtureEvents).ThenInclude(f => f.GoalScorer.PlayerPerson)
                .Include(f => f.FixtureEvents).ThenInclude(f => f.GoalAssister.PlayerPerson)
                .Include(f => f.FixtureLeague)
                .Include(f => f.FixtureScore)
                .SingleOrDefaultAsync(f => f.FixtureId == id);
        }

        public async Task<Fixture> GetFixtureResultById(long id)
        {
            return await _context.Fixtures
                .Include(f => f.HomeTeam)
                .Include(f => f.AwayTeam)
                .Include(f => f.FixtureScore)
                .SingleOrDefaultAsync(f => f.FixtureId == id);
        }

        public async Task<Fixture> GetNextFixtureByTeam(long teamId)
        {
            return await _context.Fixtures
                .Where(f => !f.isPlayed)
                .OrderBy(f => f.Date)
                .Include(f => f.HomeTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(f => f.AwayTeam).ThenInclude(t => t.CurrentTeamSheet)
                .Include(f => f.FixtureEvents).ThenInclude(f => f.GoalScorer.PlayerPerson)
                .Include(f => f.FixtureEvents).ThenInclude(f => f.GoalAssister.PlayerPerson)
                .Include(f => f.FixtureLeague)
                .Include(f => f.FixtureScore)
                .FirstOrDefaultAsync(f => f.HomeTeam.TeamId == teamId || f.AwayTeam.TeamId == teamId);
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
