
using ToDo.Application.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Infrastucture.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Infrastucture.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationDbContext _context;
        public async Task<List<Board>> GetAllAsync(int categoryId)
        {
            return await _context.Board
                .Where(b => b.CategoryId == categoryId)
                .ToListAsync();
        }
        public async Task AddAsync(Board board)
        {
            await _context.Board.AddAsync(board);
        }
        public async Task<Board?> GetByIdAsync(int id)
        {
            return await _context.Board
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Update(Board board)
        {
            _context.Board.Update(board);
        }   
        public void Delete(Board board)
        {
            _context.Board.Remove(board);
        }

       
    }
}
