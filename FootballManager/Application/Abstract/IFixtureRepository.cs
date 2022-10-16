using Application.Pagination;
using Domain.Entities;

namespace Application.Abstract
{
    public interface IFixtureRepository
    {
        Task Save();
        Task ClearLeagueFixtures(long leagueId);
        Task AddFixture(Fixture u);
        Task UpdateFixture(Fixture u);
        Task DeleteFixture(Fixture u);
        Task<Fixture> GetFixtureById(long id);
        Task<Pager<Fixture>> GetAllFixtures(int pg);
        Task<List<Fixture>> GetAllFixturesForSimulation();
        Task<Pager<Fixture>> GetAllFixturesByLeague(long leagueId, int pg);
        Task<List<Fixture>> GetAllFixturesByTeam(long teamId);
        Task<Fixture> GetNextFixtureByTeam(long teamId);
    }
}
