using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(int userId);
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task SaveChangesAsync();
        void Update(Category category);
        void Delete(Category category);
    }

}
