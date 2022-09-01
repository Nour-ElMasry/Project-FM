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
                .ForMember(fd => fd.FixtureLeagueName, opt => opt.MapFrom(f => f.FixtureLeague.Name))
                .ForMember(fd => fd.HomeTeamName, opt => opt.MapFrom(f => f.Teams[0].Name))
                .ForMember(fd => fd.AwayTeamName, opt => opt.MapFrom(f => f.Teams[1].Name))
                .ForMember(fd => fd.Venue, opt => opt.MapFrom(f => f.Venue))
                .ForMember(fd => fd.Date, opt => opt.MapFrom(f => f.Date))
                .ForMember(fd => fd.HomeTeamScore, opt => opt.MapFrom(f => f.HomeTeamScore))
                .ForMember(fd => fd.AwayTeamScore, opt => opt.MapFrom(f => f.AwayTeamScore));
        }
    }
}
