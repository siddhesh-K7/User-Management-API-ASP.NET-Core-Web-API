using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.DTOs;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(int page, int pageSize)
        {
            var query = _context.Users.AsNoTracking();
            var totalCount = await query.CountAsync();
            var users = await query
                .OrderBy(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Enumerable.Empty<User>();
            }

            name = name.ToLower();
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.FirstName.ToLower().Contains(name) || u.LastName.ToLower().Contains(name))
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> CreateUserAsync(CreateUserDto createUserDto)
        {
            // Check for duplicate email
            if (await _context.Users.AnyAsync(u => u.Email == createUserDto.Email))
            {
                return null; // Indicates duplicate
            }

            var user = new User
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                PhoneNumber = createUserDto.PhoneNumber,
                Age = createUserDto.Age,
                City = createUserDto.City,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.FirstName = updateUserDto.FirstName;
            existingUser.LastName = updateUserDto.LastName;
            existingUser.PhoneNumber = updateUserDto.PhoneNumber;
            existingUser.Age = updateUserDto.Age;
            existingUser.City = updateUserDto.City;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
