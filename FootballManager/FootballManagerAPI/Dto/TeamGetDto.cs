﻿using Domain.Entities;

namespace Application.Dto
{
    public class TeamGetDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Venue { get; set; }
        public string Logo { get; set; }
        public Person TeamManager { get; set; }
        public SeasonStats CurrentSeasonStats { get; set; }
        public TeamSheetDto CurrentTeamSheet { get; set; }
        public ShortLeagueGetDto CurrentLeague { get; set; }

    }
}
