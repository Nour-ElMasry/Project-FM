using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllPlayers : IRequest<List<Player>>
    {

    }
}
