using Application.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DataContext _context;

        public PlayerRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task AddPlayer(Player u)
        {
            await _context.Players.AddAsync(u);
        }

        public async Task DeletePlayer(Player u)
        {
            await Task.Run(() => _context.Players.Remove(u));
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            return await _context.Players
                .Include(p => p.PlayerPerson)
                .Include(p => p.PlayerStats)
                .Take(100).ToListAsync();
        }

        public async Task<Player> GetPlayerById(long id)
        {
            return await _context.Players.SingleOrDefaultAsync(p => p.PlayerId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlayer(long id, Player u)
        {
            var player = await GetPlayerById(id);

            if (player != null)
            {
                player.Position= u.Position;
                player.PlayerStats = u.PlayerStats;
            }
        }
    }
}
