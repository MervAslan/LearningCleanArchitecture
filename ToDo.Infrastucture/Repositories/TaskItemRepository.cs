using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Infrastucture.Contexts;

namespace ToDo.Infrastucture.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TaskItem item)
        {
            await _context.TaskItem.AddAsync(item);
        }

        public void Delete(TaskItem item)
        {
           _context.TaskItem.Remove(item);
        }

        public async Task<List<TaskItem>> GetAllAsync(int boardId)
        {
            return await _context.TaskItem
               .Where(b => b.BoardId == boardId)
               .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.TaskItem.FirstOrDefaultAsync(t => t.Id == id);

        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void Update(TaskItem item)
        {
            _context.TaskItem.Update(item);
        }
    }
}
