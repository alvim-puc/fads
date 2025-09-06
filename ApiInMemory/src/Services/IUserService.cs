using ApiInMemory.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiInMemory.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email); // Busca por e-mail
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(int id, User user);
        Task<User?> PatchUserAsync(int id, UserPatchRequest patchRequest);
        Task<bool> DeleteUserAsync(int id);
    }
}