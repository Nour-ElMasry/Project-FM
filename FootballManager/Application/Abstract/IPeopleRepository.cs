using Domain.Entities;

namespace Application.Abstract
{
    public interface IPeopleRepository
    {
        Task Save();
        Task AddPerson(Person u);
        Task UpdatePerson(Person u);
        Task DeletePerson(Person u);
        Task<Person> GetPersonById(long id);
        Task<List<Person>> GetAllPeople();
    }
}
