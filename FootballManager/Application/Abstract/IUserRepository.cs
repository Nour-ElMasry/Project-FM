using Domain.Entities;

namespace Application.Abstract
{
    public interface IUserRepository
    {
        Task Save();
        Task AddUser(User u);
        Task UpdateUser(long id, User u);
        Task<User> GetUserById(long id);
        Task<User> GetUserByName(string name);
        Task<List<User>> GetAllUsers();
    }
}
