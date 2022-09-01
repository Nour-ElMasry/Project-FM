using AutoMapper;
using Domain.Entities;
using FootballManagerAPI.Dto;

namespace FootballManagerAPI.Profiles
{
    public class ManagerProfile : Profile
    {
        public ManagerProfile()
        {
            CreateMap<Manager, ManagerGetDto>()
                .ForMember(md => md.ManagerId, opt => opt.MapFrom(m => m.ManagerId))
                .ForMember(md => md.ManagerPerson, opt => opt.MapFrom(m => m.ManagerPerson))
                .ForMember(md => md.CurrentTeamName, opt => opt.MapFrom(m => m.CurrentTeam.Name));

        }
    }
}
