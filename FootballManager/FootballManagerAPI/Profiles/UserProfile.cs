using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Application.Dto;

namespace Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserGetDto>()
                .ForMember(ud => ud.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(ud => ud.Username, opt => opt.MapFrom(u => u.UserName))
                .ForMember(ud => ud.UserPerson, opt => opt.MapFrom(u => u.UserPerson));


            CreateMap<UserPostDto, CreateUser>()
                .ForMember(cu => cu.Name, opt => opt.MapFrom(ud => ud.Name))
                .ForMember(cu => cu.DateOfBirth, opt => opt.MapFrom(ud => ud.DateOfBirth))
                .ForMember(cu => cu.Country, opt => opt.MapFrom(ud => ud.Country))
                .ForMember(cu => cu.Image, opt => opt.MapFrom(ud => ud.Image))
                .ForMember(cu => cu.Username, opt => opt.MapFrom(ud => ud.Username))
                .ForMember(cu => cu.Password, opt => opt.MapFrom(ud => ud.Password));

            CreateMap<UserPostDto, CreateAdmin>()
                .ForMember(cu => cu.Name, opt => opt.MapFrom(ud => ud.Name))
                .ForMember(cu => cu.DateOfBirth, opt => opt.MapFrom(ud => ud.DateOfBirth))
                .ForMember(cu => cu.Country, opt => opt.MapFrom(ud => ud.Country))
                .ForMember(cu => cu.Image, opt => opt.MapFrom(ud => ud.Image))
                .ForMember(cu => cu.Username, opt => opt.MapFrom(ud => ud.Username))
                .ForMember(cu => cu.Password, opt => opt.MapFrom(ud => ud.Password));

        }
    }
}
