using Application.Commands;
using AutoMapper;
using Domain.Entities;
using FootballManagerAPI.Dto;

namespace FootballManagerAPI.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerGetDto>()
                .ForMember(pd => pd.Id, opt => opt.MapFrom(p => p.PlayerId))
                .ForMember(pd => pd.PlayerPerson, opt => opt.MapFrom(p => p.PlayerPerson))
                .ForMember(pd => pd.CurrentTeamName, opt => opt.MapFrom(p => p.CurrentTeam.Name))
                .ForMember(pd => pd.PlayerStats, opt => opt.MapFrom(p => p.PlayerStats))
                .ForMember(pd => pd.PlayerRecord, opt => opt.MapFrom(p => p.PlayerRecord))
                .ForMember(pd => pd.Position, opt => opt.MapFrom(p => p.Position));

            CreateMap<PlayerPutPostDto, CreatePlayer>()
                .ForMember(pl => pl.Name, opt => opt.MapFrom(pd => pd.Name))
                .ForMember(pl => pl.DateOfBirth, opt => opt.MapFrom(pd => pd.DateOfBirth))
                .ForMember(pl => pl.Country, opt => opt.MapFrom(pd => pd.Country))
                .ForMember(pl => pl.Position, opt => opt.MapFrom(pd => pd.Position));
        }
    }
}
