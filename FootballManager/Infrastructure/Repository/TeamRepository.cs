using Application.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext _context;

        public TeamRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task AddTeam(Team u)
        {
            await _context.Teams.AddAsync(u);
        }

        public async Task DeleteTeam(Team u)
        {
            await Task.Run(() => _context.Teams.Remove(u));
        }

        public async Task<List<Team>> GetAllTeams()
        {
            return await _context.Teams
                .Include(t => t.TeamManager).ThenInclude(tm => tm.ManagerPerson)
                .Include(t => t.CurrentLeague)
                .Include(t => t.CurrentSeasonStats)
                .Include(t => t.CurrentTeamSheet)
                .ToListAsync();
        }

        public async Task<int> GetNumberOfTeams()
        {
            return await Task.Run(() => _context.Teams.Count());
        }

        public async Task<Team> GetTeamById(long id)
        {
            return await _context.Teams
                .Include(t => t.Players).ThenInclude(p => p.PlayerPerson)
                .Include(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(t => t.Players).ThenInclude(p => p.CurrentPlayerStats)
                .Include(t => t.Players).ThenInclude(p => p.PlayerRecord)
                .Include(t => t.TeamManager).ThenInclude(tm => tm.ManagerPerson)
                .Include(t => t.HomeFixtures)
                .Include(t => t.AwayFixtures)
                .Include(t => t.CurrentLeague)
                .Include(t => t.CurrentSeasonStats)
                .Include(t => t.CurrentTeamSheet)
                .SingleOrDefaultAsync(t => t.TeamId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateTeam(Team u)
        {
            return Task.Run(() => _context.Teams.Attach(u));
        }
    }
}
