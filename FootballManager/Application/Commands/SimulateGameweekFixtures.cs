using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class SimulateGameweekFixtures : IRequest<List<Fixture>>
    {
    }
}
