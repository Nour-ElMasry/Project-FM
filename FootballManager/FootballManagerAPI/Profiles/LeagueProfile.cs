using AutoMapper;
using Domain.Entities;
using FootballManagerAPI.Dto;

namespace FootballManagerAPI.Profiles
{
    public class LeagueProfile : Profile
    {
        public LeagueProfile()
        {
            CreateMap<League, LeagueGetDto>()
                .ForMember(ld => ld.Id, opt => opt.MapFrom(l => l.LeagueId))
                .ForMember(ld => ld.Name, opt => opt.MapFrom(l => l.Name))
                .ForMember(ld => ld.CurrentSeason, opt => opt.MapFrom(l => l.CurrentSeason));

        }
    }
}
