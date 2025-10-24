using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces
{
    public interface IBoardRepository
    {
        Task<List<Board>> GetAllAsync(int categoryId);
        Task<Board?> GetByIdAsync(int id);
        Task AddAsync(Board board);
        Task SaveChangesAsync();
        void Update(Board board);
        void Delete(Board board);
        Task<Board?> GetByNameAsync(string boardName);

    }
}
