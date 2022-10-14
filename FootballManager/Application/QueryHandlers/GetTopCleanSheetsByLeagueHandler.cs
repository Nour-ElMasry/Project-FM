using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetTopCleanSheetsByLeagueHandler : IRequestHandler<GetTopCleanSheetsByLeague, List<Player>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTopCleanSheetsByLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Player>> Handle(GetTopCleanSheetsByLeague request, CancellationToken cancellationToken)
        {
            var players = await _unitOfWork.PlayerRepository.GetTopCleanSheetsByLeague(request.LeagueId);

            if (players == null)
                return null;

            return players;
        }
    }
}
