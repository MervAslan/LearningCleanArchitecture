using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task AddAsync(TaskItem item);
        Task SaveChangesAsync();
        void Update(TaskItem item); // bu iki metod doğrudan veritabanıyla işlem yapmadığı için asenkron değil. Sadece bellekte bu nesne değişti diyor.
        void Delete(TaskItem item);
    }
}
