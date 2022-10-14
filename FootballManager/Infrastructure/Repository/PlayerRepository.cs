using Application.Abstract;
using Application.Pagination;
using Domain.Entities;
using Application.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<Pager<Player>> GetAllPlayers(int pg, PlayerFilter filter)
        {
            if (!filter.isEmpty())
            {
                var players = _context.Players.AsQueryable();

                if (filter.Team != 0)
                    players = players.Where(p => p.CurrentTeam.TeamId == filter.Team);

                if (!String.IsNullOrWhiteSpace(filter.Name))
                    players = players.Where(p => p.PlayerPerson.Name.Contains(filter.Name));

                if (!String.IsNullOrWhiteSpace(filter.Country))
                    players = players.Where(p => p.PlayerPerson.Country == filter.Country);

                if (!String.IsNullOrWhiteSpace(filter.Position))
                    players = players.Where(p => p.Position == filter.Position);

                if (filter.MinYearOfBirth != 0 && filter.MaxYearOfBirth != 0)
                    players = players.Where(p => p.PlayerPerson.BirthDate.Value.Year >= filter.MinYearOfBirth &&
                        p.PlayerPerson.BirthDate.Value.Year <= filter.MaxYearOfBirth);

                if (filter.MinYearOfBirth != 0)
                    players = players.Where(p => p.PlayerPerson.BirthDate.Value.Year >= filter.MinYearOfBirth);

                if (filter.MaxYearOfBirth != 0)
                    players = players.Where(p => p.PlayerPerson.BirthDate.Value.Year <= filter.MaxYearOfBirth);

                var pageFiltered = new Pager<Player>(await players.CountAsync(), pg);

                if (pg == 0)
                {
                    pageFiltered.PageResults = await players
                    .Include(p => p.PlayerPerson)
                    .Include(p => p.CurrentTeam)
                    .Include(p => p.CurrentPlayerStats)
                    .OrderBy(p => p.PlayerPerson.Name)
                    .ToListAsync();

                    return pageFiltered;
                }

                pageFiltered.PageResults = await players
                    .Include(p => p.PlayerPerson)
                    .Include(p => p.CurrentTeam)
                    .Include(p => p.CurrentPlayerStats)
                    .OrderBy(p => p.PlayerPerson.Name)
                    .Skip((pg - 1) * 10)
                    .Take(10)
                    .ToListAsync();

                return pageFiltered;
            }

            var page = new Pager<Player>(await _context.Players.CountAsync(), pg);

            if (pg == 0)
            {
                page.PageResults = await _context.Players
                .Include(p => p.PlayerPerson)
                .Include(p => p.CurrentTeam)
                .Include(p => p.CurrentPlayerStats)
                .OrderBy(p => p.PlayerPerson.Name)
                .ToListAsync();

                return page;
            }

            page.PageResults = await _context.Players
                .Include(p => p.PlayerPerson)
                .Include(p => p.CurrentTeam)
                .Include(p => p.CurrentPlayerStats)
                .OrderBy(p => p.PlayerPerson.Name)
                .Skip((pg - 1) * 10)
                .Take(10)
                .ToListAsync();

            return page;

        }

        public async Task<List<Player>> GetTopScorersByLeague(long leagueId)
        {
            return await _context.Players
                .Where(p => p.CurrentTeam.CurrentLeague.LeagueId == leagueId)
                .Include(p => p.PlayerPerson)
                .Include(p => p.CurrentTeam)
                .Include(p => p.PlayerRecord)
                .OrderBy(p => p.PlayerPerson.Name).ThenByDescending(p => p.PlayerRecord.Goals)
                .Take(20)
                .ToListAsync();
        }

        public async Task<List<Player>> GetTopAssistersByLeague(long leagueId)
        {
            return await _context.Players
                .Where(p => p.CurrentTeam.CurrentLeague.LeagueId == leagueId)
                .Include(p => p.PlayerPerson)
                .Include(p => p.CurrentTeam)
                .Include(p => p.PlayerRecord)
                .OrderBy(p => p.PlayerPerson.Name).ThenByDescending(p => p.PlayerRecord.Assists)
                .Take(20)
                .ToListAsync();
        }


        public async Task<List<Player>> GetTopCleanSheetsByLeague(long leagueId)
        {
            return await _context.Players
               .Where(p => p.CurrentTeam.CurrentLeague.LeagueId == leagueId)
               .Include(p => p.PlayerPerson)
               .Include(p => p.CurrentTeam)
               .Include(p => p.PlayerRecord)
               .OrderBy(p => p.PlayerPerson.Name).ThenByDescending(p => p.PlayerRecord.CleanSheets)
               .Take(20)
               .ToListAsync();
        }

        public async Task<Pager<Player>> GetAllPlayersByTeam(long teamId, int pg, PlayerFilter filter)
        {
            if (!filter.isEmpty())
            {
                var players = _context.Players.AsQueryable().Where(p => p.CurrentTeam.TeamId == teamId);

                if (filter.Team != 0)
                    players = players.Where(p => p.CurrentTeam.TeamId == filter.Team);

                if (!String.IsNullOrWhiteSpace(filter.Name))
                    players = players.Where(p => p.PlayerPerson.Name.Contains(filter.Name));

                if (!String.IsNullOrWhiteSpace(filter.Country))
                    players = players.Where(p => p.PlayerPerson.Country == filter.Country);

                if (!String.IsNullOrWhiteSpace(filter.Position))
                    players = players.Where(p => p.Position == filter.Position);

                if (filter.MinYearOfBirth != 0 && filter.MaxYearOfBirth != 0)
                    players = players.Where(p => p.PlayerPerson.BirthDate.Value.Year >= filter.MinYearOfBirth &&
                        p.PlayerPerson.BirthDate.Value.Year <= filter.MaxYearOfBirth);

                if (filter.MinYearOfBirth != 0)
                    players = players.Where(p => p.PlayerPerson.BirthDate.Value.Year >= filter.MinYearOfBirth);

                if (filter.MaxYearOfBirth != 0)
                    players = players.Where(p => p.PlayerPerson.BirthDate.Value.Year <= filter.MaxYearOfBirth);

                var pageFiltered = new Pager<Player>(await players.CountAsync(), pg);

                if (pg == 0)
                {
                    pageFiltered.PageResults = await players
                    .Include(p => p.PlayerPerson)
                    .Include(p => p.CurrentTeam)
                    .Include(p => p.CurrentPlayerStats)
                    .OrderBy(p => p.PlayerPerson.Name)
                    .ToListAsync();

                    return pageFiltered;
                }

                pageFiltered.PageResults = await players
                    .Include(p => p.PlayerPerson)
                    .Include(p => p.CurrentTeam)
                    .Include(p => p.CurrentPlayerStats)
                    .OrderBy(p => p.PlayerPerson.Name)
                    .Skip((pg - 1) * 10)
                    .Take(10)
                    .ToListAsync();

                return pageFiltered;
            }

            var page = new Pager<Player>(await _context.Players.Where(p => p.CurrentTeam.TeamId == teamId).CountAsync(), pg);

            if (pg == 0)
            {
                page.PageResults = await _context.Players
                .Where(p => p.CurrentTeam.TeamId == teamId)
                .Include(p => p.PlayerPerson)
                .Include(p => p.CurrentPlayerStats)
                .OrderBy(p => p.PlayerPerson.Name)
                .ToListAsync();

                return page;
            }

            page.PageResults = await _context.Players
                .Where(p => p.CurrentTeam.TeamId == teamId)
                .Include(p => p.PlayerPerson)
                .Include(p => p.CurrentPlayerStats)
                .OrderBy(p => p.PlayerPerson.Name)
                .Skip((pg - 1) * 10)
                .Take(10)
                .ToListAsync();

            return page;
        }

        public async Task<Player> GetPlayerById(long id)
        {
            return await _context.Players
                .Include(p => p.PlayerPerson)
                .Include(p => p.PlayerRecord)
                .Include(p => p.CurrentTeam)
                .Include(p => p.CurrentPlayerStats)
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
