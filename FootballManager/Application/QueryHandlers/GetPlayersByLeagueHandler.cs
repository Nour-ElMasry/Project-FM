using Application.Abstract;
using Application.Pagination;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetPlayersByLeagueHandler : IRequestHandler<GetPlayersByLeague, Pager<Player>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPlayersByLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pager<Player>> Handle(GetPlayersByLeague request, CancellationToken cancellationToken)
        {
            var players = await _unitOfWork.PlayerRepository.GetAllPlayersByLeague(request.LeagueId, request.Page);

            if (players.PageResults == null)
                return null;

            return players;
        }
    }
}
