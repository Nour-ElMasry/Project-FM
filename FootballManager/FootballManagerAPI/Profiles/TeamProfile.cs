using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Application.Dto;

namespace Application.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamGetDto>()
                .ForMember(td => td.Id, opt => opt.MapFrom(t => t.TeamId))
                .ForMember(td => td.Name, opt => opt.MapFrom(t => t.Name))
                .ForMember(td => td.Country, opt => opt.MapFrom(t => t.Country))
                .ForMember(td => td.Venue, opt => opt.MapFrom(t => t.Venue))
                .ForMember(td => td.Logo, opt => opt.MapFrom(t => t.Logo))
                .ForMember(td => td.TeamManager, opt => opt.MapFrom(t => t.TeamManager.ManagerPerson))
                .ForMember(td => td.CurrentTeamSheet, opt => opt.MapFrom(t => t.CurrentTeamSheet))
                .ForMember(td => td.CurrentLeague, opt => opt.MapFrom(t => new ShortLeagueGetDto { 
                    LeagueId = t.CurrentLeague.LeagueId,
                    LeagueName = t.CurrentLeague.Name, 
                }));

            CreateMap<TeamPutPostDto, CreateTeam>()
                .ForMember(td => td.Name, opt => opt.MapFrom(t => t.Name))
                .ForMember(td => td.Country, opt => opt.MapFrom(t => t.Country))
                .ForMember(td => td.Venue, opt => opt.MapFrom(t => t.Venue))
                .ForMember(td => td.Logo, opt => opt.MapFrom(t => t.Logo))
                .ForMember(td => td.LeagueId, opt => opt.MapFrom(t => t.LeagueId));
        }
    }
}
