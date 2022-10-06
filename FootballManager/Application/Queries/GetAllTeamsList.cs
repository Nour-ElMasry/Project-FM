using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllTeamsList : IRequest<List<Team>>
    {
    }
}
