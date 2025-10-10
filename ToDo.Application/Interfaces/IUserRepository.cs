using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces
{
        public interface IUserRepository
        {
            Task AddAync(User user);
            Task<User?> GetByUsernameAsync(string username);
            Task<User?> GetByEmailAsync(string email);
            Task SaveChangesAsync();

    }
}
