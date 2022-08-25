using Domain.Entities;

namespace Application.Abstract
{
    public interface IFixtureRepository
    {
        Task Save();
        Task AddFixture(Fixture u);
        Task DeleteFixture(Fixture u);
        Task<Fixture> GetFixtureById(long id);
        Task<List<Fixture>> GetAllFixtures();
    }
}
