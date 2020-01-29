using System;
using AutoMapper;
using TaskManager.Models;
using TaskManager.Models.DTO;

namespace TaskManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Projects
            CreateMap<ProjectEntity, Project>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowKey))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.PartitionKey));

            CreateMap<Project, ProjectEntity>()
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.Code));

            CreateMap<CreateProjectDto, Project>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(o => Guid.NewGuid()));

            CreateMap<UpdateProjectDto, Project>();

            //Tasks
            CreateMap<TaskEntity, TaskModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RowKey))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.PartitionKey));

            CreateMap<TaskModel, TaskEntity>()
                .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(src => src.ProjectId));

            CreateMap<TaskDto, TaskModel>();
        }
    }
}
