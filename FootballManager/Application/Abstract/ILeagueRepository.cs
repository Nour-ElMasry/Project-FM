using Application.Pagination;
using Domain.Entities;

namespace Application.Abstract
{
    public interface ILeagueRepository
    {
        Task Save();
        Task AddLeague(League u);
        Task UpdateLeague(League u);
        Task DeleteLeague(League u);
        Task<League> GetLeagueById(long id);
        Task<Pager<League>> GetAllLeagues(int pg);
        Task<List<League>> GetAllLeaguesWithTeamsForCampain();
        Task<List<League>> GetAllLeaguesWithTeamsAndPlayers();
        Task<League> GetLeagueWithTeamsById(long leagueId);
        Task<League> GetLeagueWithTeamsAndPlayersById(long leagueId);
    }
}
