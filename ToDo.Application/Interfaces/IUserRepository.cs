using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces
{
        public interface IUserRepository
        {
            Task AddAsync(User user);
            Task<User?> GetByUsernameAsync(string username);
            Task<User?> GetByEmailAsync(string email);
            Task SaveChangesAsync();
            Task<User?> GetUserWithDetailsAsync(int Id);


    }
}
