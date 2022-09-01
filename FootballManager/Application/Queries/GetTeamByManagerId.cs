using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetTeamByManagerId : IRequest<Team>
    {
        public long ManagerId { get; set; }
    }
}
