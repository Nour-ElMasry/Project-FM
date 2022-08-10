﻿using Domain.Entities.FixtureContainer;
using Domain.Entities.SeasonContainer;
using Domain.Entities.TeamContainer;
using Domain.Exceptions;

namespace Domain.Entities.LeagueContainer;
public class League
{
    public string Name { get; set; }

    public List<Team> Teams { get; set; }

    public Season CurrentSeason { get; set; }

    public List<Season> SeasonsHistory { get; set; }

    public List<Fixture> Fixtures { get; set; }

    public League(string name, List<Team> teams)
    {
        Name = name;
        Teams = teams;
        CurrentSeason = new Season(DateTime.Now.Year, this);
        SeasonsHistory = new();
        Fixtures = new(); //Will implement an algorithm to generate the fixtures
    }

    public void AddSeasonHistory(Season s) => SeasonsHistory.Add(s);
    public void NextSeason()
    {
        AddSeasonHistory(CurrentSeason);
        Teams.ForEach(t => t.ResetSeasonStats());
        CurrentSeason = new Season(++CurrentSeason.Year, this);
    }

    public void AddTeam(Team t)
    {
        if (Teams.Contains(t))
            throw new AlreadyExistsException("Team already exists in this league!");
        Teams.Add(t);
        t.CurrentLeague = this;
    }

    public void RemoveTeam(Team t)
    {
        var teamToRemove = Teams.Find(te => t == te);
        if (teamToRemove == null)
            throw new NullReferenceException("Team doesn't exist in this League!");
        Teams.Remove(teamToRemove);
    }

}

