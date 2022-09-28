using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetTeamByUserId : IRequest<Team>
    {
        public string UserId { get; set; }
    }
}
