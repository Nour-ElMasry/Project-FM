using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetNumberOfTeamsHandler : IRequestHandler<GetNumberOfTeams, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNumberOfTeamsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetNumberOfTeams request, CancellationToken cancellationToken)
        {
            if (request.LeagueId == 0)
            {
                return await _unitOfWork.TeamRepository.GetNumberOfTeams();
            }

            var leagueTeams = await _unitOfWork.TeamRepository.GetTeamsByLeagueId(request.LeagueId);

            if (leagueTeams == null)
                return 0;

            return leagueTeams.Count;
        }
    }
}
