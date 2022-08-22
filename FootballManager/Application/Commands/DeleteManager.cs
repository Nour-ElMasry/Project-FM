using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class DeleteManager : IRequest<Manager>
    {
        public long ManagerId { get; set; }
    }
}
