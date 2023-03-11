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

        public async Task<Team> GetTeamById(long id)
        {
            return await _context.Teams
                .Include(t => t.CurrentTeamSheet)
                .Include(t => t.CurrentLeague)
                .SingleOrDefaultAsync(t => t.TeamId == id);
        }

        public async Task<List<Team>> GetTeamsByLeagueId(long leagueId)
        {
            return await _context.Teams.Where(t => t.CurrentLeague.LeagueId == leagueId)
            .Include(t => t.CurrentSeasonStats)
            .OrderByDescending(t => t.CurrentSeasonStats.Points).ThenByDescending(t => t.CurrentSeasonStats.GoalsFor - t.CurrentSeasonStats.GoalsAgainst).ThenBy(t => t.Name)
            .ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateTeam(Team u)
        {
            return Task.Run(() => _context.Teams.Attach(u));
        }

        public async Task<List<Team>> GetTeamsNotAssignedToLeagues()
        {
            return await _context.Teams.Include(t => t.CurrentTeamSheet).Where(t => t.CurrentLeague == null).ToListAsync();
        }
    }
}
