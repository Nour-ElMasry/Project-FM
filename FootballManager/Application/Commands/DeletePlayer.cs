using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class DeletePlayer : IRequest<Player>
    {
        public long PlayerId { get; set; }
    }
}
