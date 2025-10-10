using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ToDo.Application.CQRS.Commands;
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
        }
    }
}
