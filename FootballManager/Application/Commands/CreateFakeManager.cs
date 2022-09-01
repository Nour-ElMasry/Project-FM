using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateFakeManager : IRequest<FakeManager>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }

    }
}
