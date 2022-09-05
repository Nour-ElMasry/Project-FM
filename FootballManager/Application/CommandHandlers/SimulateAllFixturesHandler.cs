using Application.Abstract;
using Application.Commands;
using MediatR;

namespace Application.CommandHandlers
{
    public class SimulateAllFixturesHandler : IRequestHandler<SimulateAllFixtures>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SimulateAllFixturesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(SimulateAllFixtures request, CancellationToken cancellationToken)
        {
            var fixtures = await _unitOfWork.FixtureRepository.GetAllFixtures();

            if (fixtures != null)
            {
                var leagueFixtures = fixtures.Where(f => f.FixtureLeague.LeagueId == request.LeagueId).ToList();

                leagueFixtures.ForEach(f => f.SimulateFixture());

                await _unitOfWork.Save();
            }

            return new Unit();
        }
    }
}
