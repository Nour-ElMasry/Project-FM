using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllLeagues : IRequest<List<League>>
    {
    }
}
