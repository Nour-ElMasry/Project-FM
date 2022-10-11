using Application.Pagination;
using Domain.Entities;
using Application.Filters;

namespace Application.Abstract
{
    public interface IPlayerRepository
    {
        Task Save();
        Task AddPlayer(Player u);
        Task UpdatePlayer(Player u);
        Task DeletePlayer(Player u);
        Task<Player> GetPlayerById(long id);
        Task<Pager<Player>> GetAllPlayers(int pg, PlayerFilter filter);
        Task<Pager<Player>> GetAllPlayersByLeague(long leagueId, int pg);
        Task<Pager<Player>> GetAllPlayersByTeam(long teamId, int pg, PlayerFilter filter);
    }
}
