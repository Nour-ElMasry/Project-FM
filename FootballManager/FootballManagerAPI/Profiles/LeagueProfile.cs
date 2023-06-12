using AutoMapper;
using Domain.Entities;
using Application.Dto;

namespace Application.Profiles
{
    public class LeagueProfile : Profile
    {
        public LeagueProfile()
        {
            CreateMap<League, LeagueGetDto>()
                .ForMember(ld => ld.Id, opt => opt.MapFrom(l => l.LeagueId))
                .ForMember(ld => ld.Name, opt => opt.MapFrom(l => l.Name))
                .ForMember(ld => ld.LeagueLogo, opt => opt.MapFrom(l => l.LeagueLogo))
                .ForMember(ld => ld.CurrentSeason, opt => opt.MapFrom(l => l.CurrentSeason));

            CreateMap<League, CampainLeaguesTeamsDto>()
                .ForMember(ld => ld.LeagueId, opt => opt.MapFrom(l => l.LeagueId))
                .ForMember(ld => ld.LeagueName, opt => opt.MapFrom(l => l.Name))
                .ForMember(ld => ld.LeagueLogo, opt => opt.MapFrom(l => l.LeagueLogo))
                .ForMember(ld => ld.LeagueTeams, opt => opt.MapFrom(l => l.Teams.Select(team => new ShortTeamGetDto
                {
                    TeamId= team.TeamId,
                    TeamName = team.Name,
                    TeamLogo = team.Logo,
                })));

        }
    }
}
