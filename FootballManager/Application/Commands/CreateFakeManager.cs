using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateFakeManager : IRequest<FakeManager>
    {
        public long ManagerPersonId { get; set; }

    }
}
