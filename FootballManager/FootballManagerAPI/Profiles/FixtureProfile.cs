using Application.Commands;
using AutoMapper;
using Domain.Entities;
using FootballManagerAPI.Dto;

namespace FootballManagerAPI.Profiles
{
    public class FixtureProfile : Profile
    {
        public FixtureProfile()
        {
            CreateMap<Fixture, FixtureGetDto>()
                .ForMember(fd => fd.Id, opt => opt.MapFrom(f => f.FixtureId))
                .ForMember(fd => fd.FixtureLeague, opt => opt.MapFrom(f => new ShortLeagueGetDto { 
                    LeagueId = f.FixtureLeague.LeagueId,
                    LeagueName = f.FixtureLeague.Name,  
                }))
                .ForMember(fd => fd.HomeTeam, opt => opt.MapFrom(f => new ShortTeamGetDto { 
                    TeamId = f.HomeTeam.TeamId,
                    TeamName = f.HomeTeam.Name,
                    TeamLogo = f.HomeTeam.Logo,
                }))
                .ForMember(fd => fd.AwayTeam, opt => opt.MapFrom(f => new ShortTeamGetDto
                {
                    TeamId = f.AwayTeam.TeamId,
                    TeamName = f.AwayTeam.Name,
                    TeamLogo = f.AwayTeam.Logo,
                }))
                .ForMember(fd => fd.Venue, opt => opt.MapFrom(f => f.Venue))
                .ForMember(fd => fd.Date, opt => opt.MapFrom(f => f.Date))
                .ForMember(fd => fd.FixtureScore, opt => opt.MapFrom(f => f.FixtureScore))
                .ForMember(fd => fd.FixtureEvents, opt => opt.MapFrom(f => f.FixtureEvents.Select(e => new EventGetDto 
                { 
                    PlayerScorer = new ShortPlayerGetDto 
                    {
                        Id = e.GoalScorer.PlayerId,
                        PlayerPerson = e.GoalScorer.PlayerPerson,
                        Position = e.GoalScorer.Position,
                        CurrentTeam = new ShortTeamGetDto
                        {
                            TeamId = e.GoalScorer.CurrentTeam.TeamId,
                            TeamName = e.GoalScorer.CurrentTeam.Name,
                            TeamLogo = e.GoalScorer.CurrentTeam.Logo,
                        },
                    },
                    PlayerAssister = new ShortPlayerGetDto
                    {
                        Id = e.GoalAssister.PlayerId,
                        PlayerPerson = e.GoalAssister.PlayerPerson,
                        Position = e.GoalAssister.Position,
                        CurrentTeam = new ShortTeamGetDto
                        {
                            TeamId = e.GoalAssister.CurrentTeam.TeamId,
                            TeamName = e.GoalAssister.CurrentTeam.Name,
                            TeamLogo = e.GoalAssister.CurrentTeam.Logo,
                        },
                    }
                })))
                .ForMember(fd => fd.isPlayed, opt => opt.MapFrom(f => f.isPlayed));

            CreateMap<FixturePutDto, UpdateFixture>()
                .ForMember(f => f.newDate, opt => opt.MapFrom(fd => fd.Date));
        }
    }
}
