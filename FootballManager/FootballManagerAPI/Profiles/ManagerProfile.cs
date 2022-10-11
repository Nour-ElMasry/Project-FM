using AutoMapper;
using Domain.Entities;
using Application.Dto;

namespace Application.Profiles
{
    public class ManagerProfile : Profile
    {
        public ManagerProfile()
        {
            CreateMap<Manager, ManagerGetDto>()
                .ForMember(md => md.ManagerId, opt => opt.MapFrom(m => m.ManagerId))
                .ForMember(md => md.ManagerPerson, opt => opt.MapFrom(m => m.ManagerPerson))
                .ForMember(md => md.CurrentTeam, opt => opt.MapFrom(m => new ShortTeamGetDto
                {
                    TeamId = m.CurrentTeam.TeamId,
                    TeamName = m.CurrentTeam.Name,
                    TeamLogo = m.CurrentTeam.Logo
                }));
        }
    }
}
