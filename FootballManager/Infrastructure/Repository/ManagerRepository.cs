using Application.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly DataContext _context;

        public ManagerRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task AddManager(Manager u)
        {
            await _context.Managers.AddAsync(u);
        }

        public async Task DeleteManager(Manager u)
        {
            await Task.Run(() => _context.Managers.Remove(u));
        }

        public async Task<List<Manager>> GetAllManagers()
        {
            return await _context.Managers
                .Include(m => m.ManagerPerson)
                .Include(m => m.CurrentTeam)
                .Take(100).ToListAsync();
        }

        public async Task<Manager> GetManagerById(long id)
        {
            return await _context.Managers
                .Include(m => m.ManagerPerson)
                .Include(m => m.CurrentTeam)
                .SingleOrDefaultAsync(m => m.ManagerId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
