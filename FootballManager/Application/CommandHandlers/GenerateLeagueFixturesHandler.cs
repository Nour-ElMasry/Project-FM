using Application.Abstract;
using Application.Commands;
using MediatR;

namespace Application.CommandHandlers
{
    public class GenerateLeagueFixturesHandler : IRequestHandler<GenerateLeagueFixtures>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateLeagueFixturesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(GenerateLeagueFixtures request, CancellationToken cancellationToken)
        {
            var league = await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);

            if (league != null) {
                league.CreateFixtures();

                league.Fixtures.ForEach(async f => await _unitOfWork.FixtureRepository.AddFixture(f));
                await _unitOfWork.Save();
            }

            
            return new Unit();
        }
    }
}
