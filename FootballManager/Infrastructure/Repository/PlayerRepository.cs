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
                .Include(p => p.PlayerRecord)
                .Include(p => p.CurrentTeam)
                .Include(p => p.PlayerStats)
                .Take(100).ToListAsync();
        }

        public async Task<Player> GetPlayerById(long id)
        {
            return await _context.Players
                .Include(p => p.PlayerPerson)
                .Include(p => p.PlayerRecord)
                .Include(p => p.CurrentTeam)
                .Include(p => p.PlayerStats)
                .SingleOrDefaultAsync(p => p.PlayerId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdatePlayer(Player u)
        {
            return Task.Run(() => _context.Players.Attach(u));
        }
    }
}
