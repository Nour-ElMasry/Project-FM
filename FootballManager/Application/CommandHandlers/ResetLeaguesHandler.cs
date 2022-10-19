using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class ResetLeaguesHandler : IRequestHandler<ResetLeagues, List<League>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResetLeaguesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<League>> Handle(ResetLeagues request, CancellationToken cancellationToken)
        {
            var leagues = await _unitOfWork.LeagueRepository.GetAllLeaguesWithTeamsAndPlayers();

            if (leagues == null)
                return null;

            await _unitOfWork.FixtureRepository.ClearFixtures();

            leagues.ForEach(l =>
            {
                l.ResetLeague();
            });

            await _unitOfWork.Save();

            return leagues;
        }
    }
}