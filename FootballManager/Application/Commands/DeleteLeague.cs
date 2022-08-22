using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class DeleteLeague : IRequest<League>
    {
        public long LeagueId { get; set; }
    }
}
