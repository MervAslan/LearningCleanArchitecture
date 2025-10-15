using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Interfaces;
using ToDo.Infrastucture.Contexts;
using ToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Infrastucture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user); 
            
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetUserWithDetailsAsync(int Id)
        {
            return await _context.Users
                .Include(u => u.Categories) //categories burda navigation property ismi  (users entity sinde tanımlı olan)
                    .ThenInclude(c => c.Boards)
                        .ThenInclude(b => b.Tasks)
                .FirstOrDefaultAsync(u => u.Id == Id);
        }

    }
}
