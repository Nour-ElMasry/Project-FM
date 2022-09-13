﻿using Domain.Entities;

namespace FootballManagerAPI.Dto
{
    public class TeamGetDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Venue { get; set; }
        public Person TeamManager { get; set; }
        public SeasonStats CurrentSeasonStats { get; set; }
        public TeamSheet CurrentTeamSheet { get; set; }
        public string CurrentLeague { get; set; }

    }
}