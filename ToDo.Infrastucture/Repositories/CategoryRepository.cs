using ToDo.Application.Interfaces;
using ToDo.Infrastucture.Contexts;
using ToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Infrastucture.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Category category)
        {
            await _context.Category.AddAsync(category);
        }
        public async Task<List<Category>> GetAllAsync(int userId)
        {
            return await _context.Category
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Category
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Update(Category category)
        {
            _context.Category.Update(category);
        }
        public void Delete(Category category)
        {
            _context.Category.Remove(category);
        }
        public async Task<Category?> GetByNameAndUserIdAsync(string categoryName, int userId)
        {
            return await _context.Category
                .FirstOrDefaultAsync(c => c.Name == categoryName && c.UserId == userId);
        }

    }
}
