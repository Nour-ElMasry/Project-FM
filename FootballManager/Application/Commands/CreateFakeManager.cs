using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateFakeManager : IRequest<FakeManager>
    {
        public Person ManagerPerson { get; set; }

    }
}
