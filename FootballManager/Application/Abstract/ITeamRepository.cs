using Domain.Entities;

namespace Application.Abstract
{
    public interface ITeamRepository
    {
        Task Save();
        Task<Team> GetTeamById(long id);
        Task<List<Team>> GetTeamsByLeagueId(long leagueId);
        Task<List<Team>> GetTeamsNotAssignedToLeagues();
    }
}
