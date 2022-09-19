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
        Task<List<Fixture>> GetAllFixtures();
        Task<List<Fixture>> GetAllFixturesForSimulation();
        Task<List<Fixture>> GetAllFixturesByLeague(long leagueId);
    }
}
