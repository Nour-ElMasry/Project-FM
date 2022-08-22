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
        Task<List<Player>> GetAllPlayers();
    }
}
