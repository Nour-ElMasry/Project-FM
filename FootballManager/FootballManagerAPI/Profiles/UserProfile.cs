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
            CreateMap<User, UserGetDto> ()
                .ForMember(ud => ud.Id, opt => opt.MapFrom(u => u.UserId))
                .ForMember(ud => ud.Username, opt => opt.MapFrom(u => u.Username))
                .ForMember(ud => ud.UserPerson, opt => opt.MapFrom(u => u.UserPerson));
        }
    }
}
