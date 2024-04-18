using RedisDotnet.Models;

namespace RedisDotnet.Data
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
    }
}

