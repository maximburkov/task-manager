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

            //Tokens
            CreateMap<RefreshTokenEntity, RefreshToken>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.RowKey))
                .ForMember(dest => dest.UserLogin, opt => opt.MapFrom(src => src.PartitionKey));

            CreateMap<RefreshToken, RefreshTokenEntity>()
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.UserLogin));

        }
    }
}
