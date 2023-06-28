using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetTeamWithLineup : IRequest<Team>
    {
        public long TeamId { get; set; }
    }
}

