using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class NextSeason : IRequest<League>
    {
        public long LeagueId { get; set; }
    }
}
