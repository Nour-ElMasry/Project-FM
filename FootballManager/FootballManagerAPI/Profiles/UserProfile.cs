using Application.Commands;
using AutoMapper;
using Domain.Entities;
using FootballManagerAPI.Dto;

namespace FootballManagerAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAuthDto, AuthUser>()
                .ForMember(au => au.UserName, opt => opt.MapFrom(aud => aud.Username))
                .ForMember(au => au.Password, opt => opt.MapFrom(aud => aud.Password));
        }
    }
}
