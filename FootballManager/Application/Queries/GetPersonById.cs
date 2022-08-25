using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetPersonById : IRequest<Person>
    {
        public long PersonId { get; set; }
    }
}
