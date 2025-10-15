using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Interfaces
{
    public interface ICurrentUserService
    {
      public int CurrentUserId { get; }
    }
}
