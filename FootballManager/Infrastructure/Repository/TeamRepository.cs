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
                .Include(t => t.Players)
                .Take(100).ToListAsync();
        }

        public async Task<Team> GetTeamById(long id)
        {
            return await _context.Teams
                .Include(t => t.Players)
                .SingleOrDefaultAsync(t => t.TeamId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeam(long id, Team u)
        {
            var team = await GetTeamById(id);

            if (team != null)
            {
                team.Name = u.Name;
                team.Venue = u.Venue;
                team.Country = u.Country;
                team.TeamManager = u.TeamManager;
            }
        }
    }
}
