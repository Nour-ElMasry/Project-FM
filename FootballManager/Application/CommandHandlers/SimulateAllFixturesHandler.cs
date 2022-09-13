using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class SimulateAllFixturesHandler : IRequestHandler<SimulateAllFixtures, List<Fixture>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SimulateAllFixturesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Fixture>> Handle(SimulateAllFixtures request, CancellationToken cancellationToken)
        {
            var league = await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);

            if(league == null)
                return null;

            var fixtures = await _unitOfWork.FixtureRepository.GetAllFixtures();

            var leagueFixtures = fixtures.Where(f => f.FixtureLeague.LeagueId == league.LeagueId).ToList();
            leagueFixtures.ForEach(f => f.SimulateFixture());
            await _unitOfWork.Save();

            return leagueFixtures;
        }
    }
}
