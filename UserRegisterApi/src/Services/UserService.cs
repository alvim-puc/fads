using MongoDB.Driver;
using UserRegisterApi.Models;

namespace UserRegisterApi.Services
{

    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMongoCollection<User> users)
        {
            _users = users;
        }

        public async Task<List<User>> GetAllAsync() =>
            await _users.Find(_ => true).ToListAsync();

        public async Task<User?> GetByEmailAsync(string email) =>
            await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task CreateAsync(User user) =>
            await _users.InsertOneAsync(user);

        public async Task UpdateAsync(string id, User updated) =>
            await _users.ReplaceOneAsync(u => u.Id == id, updated);

        public async Task DeleteAsync(string id) =>
            await _users.DeleteOneAsync(u => u.Id == id);
    }
}