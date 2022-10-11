using Domain.Entities;

namespace Application.Abstract
{
    public interface IManagerRepository
    {
        Task Save();
        Task AddManager(Manager u);
        Task DeleteManager(Manager u);
        Task<Manager> GetManagerById(long id);
        Task<List<Manager>> GetAllManagers();
        Task<bool> UserHasTeam(User u);
    }
}
