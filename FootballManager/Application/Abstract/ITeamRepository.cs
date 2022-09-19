using Domain.Entities;

namespace Application.Abstract
{
    public interface ITeamRepository
    {
        Task Save();
        Task AddTeam(Team u);
        Task<int> GetNumberOfTeams();
        Task UpdateTeam(Team u);
        Task DeleteTeam(Team u);
        Task<Team> GetTeamById(long id);
        Task<List<Team>> GetTeamsByLeagueId(long leagueId);
        Task<List<Team>> GetAllTeams();
    }
}
