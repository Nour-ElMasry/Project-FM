using Domain.Entities;

namespace Application.Abstract
{
    public interface ILeagueRepository
    {
        Task Save();
        Task AddLeague(League u);
        Task UpdateLeague(long id, League u);
        Task DeleteLeague(League u);
        Task<League> GetLeagueById(long id);
        Task<List<League>> GetAllLeagues();
    }
}
