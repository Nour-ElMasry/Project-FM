using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetTopAssistersByLeagueHandler : IRequestHandler<GetTopAssistersByLeague, List<Player>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTopAssistersByLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Player>> Handle(GetTopAssistersByLeague request, CancellationToken cancellationToken)
        {
            var players = await _unitOfWork.PlayerRepository.GetTopAssistersByLeague(request.LeagueId);

            if (players == null)
                return null;

            return players;
        }
    }
}
