using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetPlayersByLeagueHandler : IRequestHandler<GetPlayersByLeague, List<Player>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPlayersByLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Player>> Handle(GetPlayersByLeague request, CancellationToken cancellationToken)
        {
            var players = await _unitOfWork.PlayerRepository.GetAllPlayersByLeague(request.LeagueId);

            if (players == null)
                return null;

            return players;
        }
    }
}
