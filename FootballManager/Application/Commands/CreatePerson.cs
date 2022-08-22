using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreatePerson : IRequest<Person>
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Country { get; set; }
    }
}
