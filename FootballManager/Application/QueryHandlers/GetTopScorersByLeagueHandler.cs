using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetTopScorersByLeagueHandler : IRequestHandler<GetTopScorersByLeague, List<Player>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTopScorersByLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Player>> Handle(GetTopScorersByLeague request, CancellationToken cancellationToken)
        {
            var players = await _unitOfWork.PlayerRepository.GetTopScorersByLeague(request.LeagueId);

            if (players == null)
                return null;

            return players;
        }
    }
}
