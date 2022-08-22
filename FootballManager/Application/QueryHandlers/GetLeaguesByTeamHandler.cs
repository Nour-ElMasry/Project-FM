using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetLeaguesByTeamHandler : IRequestHandler<GetLeaguesByTeam, List<League>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLeaguesByTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<League>> Handle(GetLeaguesByTeam request, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);
            return team.Leagues ;
        }
    }
}
