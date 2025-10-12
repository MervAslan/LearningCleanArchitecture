using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces
{
        public interface IUserRepository
        {
            Task AddAync(User user);
            Task<User?> GetByUsernameAsync(string username);
            Task<User?> GetByEmailAsync(string email);
            Task SaveChangesAsync();
            Task<User?> GetUserWithDetailsAsync(string email);


    }
}
