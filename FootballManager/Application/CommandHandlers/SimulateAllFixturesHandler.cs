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
            var league = await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);

            if (league != null)
            {
                league.Fixtures.ForEach(f => f.SimulateFixture());

                await _unitOfWork.Save();
            }
            return new Unit();
        }
    }
}
