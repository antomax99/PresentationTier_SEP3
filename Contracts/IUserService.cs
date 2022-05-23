using Entities;

namespace Contracts;

public interface IUserService
{
    public Task<IList<User>> GetAsync();
    public Task<User> GetUserByUsername(string username);
    public Task<User> GetUserById(int id);
    public Task AddUserAsync(User user);
    public Task DeleteAsync(int id);
    public Task UpdateAsync(User user);
}