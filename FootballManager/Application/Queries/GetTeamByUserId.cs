using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetTeamByUserId : IRequest<Team>
    {
        public long UserId { get; set; }
    }
}
