using Application.Abstract;
using Application.Pagination;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetPlayersByTeamHandler : IRequestHandler<GetPlayersByTeam, Pager<Player>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPlayersByTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pager<Player>> Handle(GetPlayersByTeam request, CancellationToken cancellationToken)
        {
            var players = await _unitOfWork.PlayerRepository.GetAllPlayersByTeam(request.TeamId, request.Page, request.Filter);

            if(players == null)
                return null;

            return players;
        }
    }
}
