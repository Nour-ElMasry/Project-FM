using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetTeamById : IRequest<Team>
    {
        public long TeamId { get; set; }
    }
}
