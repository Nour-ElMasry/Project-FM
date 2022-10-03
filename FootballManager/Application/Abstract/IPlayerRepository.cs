using Application.Pagination;
using Domain.Entities;

namespace Application.Abstract
{
    public interface IPlayerRepository
    {
        Task Save();
        Task AddPlayer(Player u);
        Task UpdatePlayer(Player u);
        Task DeletePlayer(Player u);
        Task<Player> GetPlayerById(long id);
        Task<Pager<Player>> GetAllPlayers(int pg);
        Task<Pager<Player>> GetAllPlayersByLeague(long leagueId, int pg);
        Task<Pager<Player>> GetAllPlayersByTeam(long teamId, int pg);
    }
}
