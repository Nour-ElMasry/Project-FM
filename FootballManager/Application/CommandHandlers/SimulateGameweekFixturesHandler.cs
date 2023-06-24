using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class SimulateGameweekFixturesHandler : IRequestHandler<SimulateGameweekFixtures, List<Fixture>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SimulateGameweekFixturesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Fixture>> Handle(SimulateGameweekFixtures request, CancellationToken cancellationToken)
        {
            var fixtures = await _unitOfWork.FixtureRepository.GetAllFixturesForSimulationByGameWeek();
            
            if(fixtures == null)
                return null;

            fixtures.ForEach(f => f.SimulateFixture());

            await _unitOfWork.Save();

            return fixtures;
        }
    }
}
