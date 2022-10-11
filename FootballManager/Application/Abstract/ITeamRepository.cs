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
        Task<Pager<Team>> GetTeamsByLeagueId(long leagueId, int pg);
        Task<Pager<Team>> GetAllTeams(int pg);
        Task<List<Team>> GetTeamsList();
    }
}
