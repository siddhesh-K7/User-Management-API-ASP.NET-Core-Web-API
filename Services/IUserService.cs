using UserManagementAPI.DTOs;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public interface IUserService
    {
        Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(int page, int pageSize);
        Task<IEnumerable<User>> SearchUsersAsync(string name);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> CreateUserAsync(CreateUserDto createUserDto);
        Task<bool> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
