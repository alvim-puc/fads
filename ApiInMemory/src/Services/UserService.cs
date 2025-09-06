using ApiInMemory.Models;
using ApiInMemory.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace ApiInMemory.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            // Aplica regras de negócio
            if (!user.IsValid())
                throw new ArgumentException("Usuário inválido");
            
            // ID será gerado automaticamente pelo Entity Framework
            user.Id = 0; // Garante que será auto increment
            user.UpdateTimestamps();
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return null;

            // Atualiza apenas campos permitidos
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.UpdatedAt = DateTime.UtcNow;
            
            if (!existingUser.IsValid())
                throw new ArgumentException("Usuário inválido");
            
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User?> PatchUserAsync(int id, UserPatchRequest patchRequest)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return null;

            // Atualiza apenas campos que foram fornecidos (não nulos)
            if (!string.IsNullOrEmpty(patchRequest.Name))
                user.Name = patchRequest.Name;
                
            if (!string.IsNullOrEmpty(patchRequest.Email))
                user.Email = patchRequest.Email;
                
            if (!string.IsNullOrEmpty(patchRequest.Password))
                user.Password = patchRequest.Password;
                
            if (patchRequest.PersonCode.HasValue)
                user.PersonCode = patchRequest.PersonCode.Value;
                
            if (!string.IsNullOrEmpty(patchRequest.PasswordHint))
                user.PasswordHint = patchRequest.PasswordHint;
                
            if (patchRequest.Age.HasValue)
                user.Age = patchRequest.Age.Value;
                
            if (patchRequest.Gender.HasValue)
                user.Gender = patchRequest.Gender.Value;

            user.UpdateTimestamps();
            
            if (!user.IsValid())
                throw new ArgumentException("Usuário inválido após atualização");
            
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<User?> PatchUserAsync(int id, JsonPatchDocument<User> patchDoc)
        {
            throw new NotImplementedException();
        }
    }
}