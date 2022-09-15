using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class GenerateLeagueFixturesHandler : IRequestHandler<GenerateLeagueFixtures, List<Fixture>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateLeagueFixturesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Fixture>> Handle(GenerateLeagueFixtures request, CancellationToken cancellationToken)
        {
            var league = await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);

            if (league == null)
                return null;

            await _unitOfWork.FixtureRepository.Clear();

            league.Fixtures = null;

            await Task.Run(() => league.CreateFixtures());

            await _unitOfWork.Save();

            return league.Fixtures;
        }
    }
}
