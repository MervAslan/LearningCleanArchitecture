using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces
{
    public interface IToDoRepository
    {
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task AddAsync(TaskItem item);
        void Update(TaskItem item); // bu iki metod doğrudan veritabanıyla işlem yapmadığı için asenkron değil. Sadece bellekte bu nesne değişti diyor.
        void Delete(TaskItem item);
    }
}
