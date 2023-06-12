using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllLeaguesForCampain : IRequest<List<League>>
    {
    }
}
