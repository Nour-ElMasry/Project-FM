using Application.Abstract;
using Application.Commands;
using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class NextSeasonHandler : IRequestHandler<NextSeason, List<League>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public NextSeasonHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<League>> Handle(NextSeason request, CancellationToken cancellationToken)
        {
            var leagues = await _unitOfWork.LeagueRepository.GetAllLeaguesWithTeamsAndPlayers();

            if (leagues == null)
                return null;

            await _unitOfWork.FixtureRepository.ClearFixtures();

            leagues.ForEach(async l =>
            {
                await Task.Run(() => l.NextSeason());
            });

            await _unitOfWork.Save();

            return leagues;
        }
    }
}