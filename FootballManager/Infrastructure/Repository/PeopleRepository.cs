using Application.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly DataContext _context;

        public PeopleRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task AddPerson(Person u)
        {
            await _context.People.AddAsync(u);
        }

        public async Task DeletePerson(Person u)
        {
            await Task.Run(() => _context.People.Remove(u));
        }

        public async Task<List<Person>> GetAllPeople()
        {
            return await _context.People
                .Take(100).ToListAsync();
        }

        public async Task<Person> GetPersonById(long id)
        {
            return await _context.People.SingleOrDefaultAsync(p => p.PersonId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePerson(long id, Person u)
        {
            var person = await GetPersonById(id);

            if (person != null)
            {
                person.Name = u.Name;
                person.BirthDate = u.BirthDate;
                person.Country = u.Country;
            }
        }
    }
}
