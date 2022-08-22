using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllFixtures : IRequest<List<Fixture>>
    {
    }
}
