using Application.Pagination;
using Domain.Entities;

namespace Application.Abstract
{
    public interface ITeamRepository
    {
        Task Save();
        Task AddTeam(Team u);
        Task UpdateTeam(Team u);
        Task DeleteTeam(Team u);
        Task<Team> GetTeamById(long id);
        Task<List<Team>> GetTeamsByLeagueId(long leagueId);
        Task<Pager<Team>> GetAllTeams(int pg);
        Task<List<Team>> GetTeamsNotAssignedToLeagues();
        Task<List<Team>> GetTeamsList();
    }
}
