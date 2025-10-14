using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;

namespace ToDo.Application.CQRS.Commands.CategoryCommands
{
    public class CreateCategoryCommand : IRequest<CategoryDto>
    {
       

    }
}
