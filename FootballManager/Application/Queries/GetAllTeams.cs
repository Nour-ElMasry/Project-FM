using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllTeams : IRequest<List<Team>>
    {
    }
}
