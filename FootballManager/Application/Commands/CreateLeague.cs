using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateLeague : IRequest<League>
    {
        public string Name { get; set; }
    }
}
