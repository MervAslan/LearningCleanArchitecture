using AutoMapper;
using ToDo.Application.CQRS.Commands.UserCommands;
using ToDo.Application.CQRS.Commands.CategoryCommands;
using ToDo.Application.DTOs;
using ToDo.Domain.Entities;

namespace ToDo.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<User, UserDto>(); // dbden gelen  user entitysini frontende döndürmek için userdtoya dönüştürüyoruz
            CreateMap<Category, CategoryDto>();
            CreateMap<Board, BoardDto>();
            CreateMap<TaskItem, TaskItemDto>();
            CreateMap<CreateCategoryCommand, Category>();

        }
    } 
}
