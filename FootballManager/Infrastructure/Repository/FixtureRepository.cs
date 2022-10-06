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

        public async Task ClearLeagueFixtures(long leagueId)
        {
            await Task.Run(() => _context.Fixtures.RemoveRange(_context.Fixtures.Where(f => f.FixtureLeague.LeagueId == leagueId).ToList()));
        }

        public async Task DeleteFixture(Fixture u)
        {
            await Task.Run(() => _context.Fixtures.Remove(u));
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
