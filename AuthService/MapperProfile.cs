using System;
using AuthService.Models;
using AuthService.Models.DTO;
using AuthService.Models.TableStorage;
using AutoMapper;

namespace TaskManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Users
            CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.RowKey));

            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Login));

            CreateMap<CreateUserRequest, User>();
        }
    }
}
