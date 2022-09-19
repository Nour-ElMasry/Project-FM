using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetTeamsByLeagueHandler : IRequestHandler<GetTeamsByLeague, List<Team>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTeamsByLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Team>> Handle(GetTeamsByLeague request, CancellationToken cancellationToken)
        {
            var teams = await _unitOfWork.TeamRepository.GetTeamsByLeagueId(request.LeagueId);

            return teams;
        }
    }
}
