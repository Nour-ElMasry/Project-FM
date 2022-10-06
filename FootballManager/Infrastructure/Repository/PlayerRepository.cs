﻿using Application.Abstract;
using Application.Pagination;
using Domain.Entities;
using FootballManagerAPI.Filters;
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

        public async Task<Pager<Player>> GetAllPlayers(int pg, PlayerFilter filter)
        {
            var players = from p in _context.Players
                          select p;

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

            var page = new Pager<Player>(await players.CountAsync(), pg);

            if (pg == 0)
            {
                page.PageResults = await players
                .Include(p => p.PlayerPerson)
                .Include(p => p.PlayerRecord)
                .Include(p => p.CurrentTeam)
                .Include(p => p.CurrentPlayerStats)
                .OrderBy(p => p.PlayerPerson.Name)
                .ToListAsync();

                return page;
            }

            page.PageResults = await players
                .Include(p => p.PlayerPerson)
                .Include(p => p.PlayerRecord)
                .Include(p => p.CurrentTeam)
                .Include(p => p.CurrentPlayerStats)
                .OrderBy(p => p.PlayerPerson.Name)
                .Skip((pg - 1) * 10)
                .Take(10)
                .ToListAsync();

            return page;
        }

        public async Task<Pager<Player>> GetAllPlayersByLeague(long leagueId, int pg)
        {
            var page = new Pager<Player>(await _context.Players.Where(p => p.CurrentTeam.CurrentLeague.LeagueId == leagueId).CountAsync(), pg);

            if (pg == 0)
            {
                page.PageResults = await _context.Players
                .Where(p => p.CurrentTeam.CurrentLeague.LeagueId == leagueId)
                .Include(p => p.PlayerPerson)
                .Include(p => p.CurrentTeam)
                .ToListAsync();

                return page;
            }

            page.PageResults = await _context.Players
                .Where(p => p.CurrentTeam.CurrentLeague.LeagueId == leagueId)
                .Include(p => p.PlayerPerson)
                .Include(p => p.CurrentTeam)
                .Skip((pg - 1) * 10)
                .Take(10)
                .ToListAsync();

            return page;
        }

        public async Task<Pager<Player>> GetAllPlayersByTeam(long teamId, int pg, PlayerFilter filter)
        {
            var players = from p in _context.Players
                          where p.CurrentTeam.TeamId == teamId
                          select p;

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

            var page = new Pager<Player>(await players.CountAsync(), pg);

            if (pg == 0)
            {
                page.PageResults = await players
                .Include(p => p.PlayerPerson)
                .Include(p => p.CurrentTeam)
                .ToListAsync();

                return page;
            }

            page.PageResults = await players
                .Include(p => p.PlayerPerson)
                .Include(p => p.CurrentTeam)
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
