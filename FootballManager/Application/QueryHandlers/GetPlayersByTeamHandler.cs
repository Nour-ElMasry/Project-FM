using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetPlayersByTeamHandler : IRequestHandler<GetPlayersByTeam, List<Player>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPlayersByTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Player>> Handle(GetPlayersByTeam request, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);
            return team.Players;
        }
    }
}
