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
            var league = await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);

            if(league != null)
            {
                var teams = await _unitOfWork.TeamRepository.GetAllTeams();
                var leagueTeams = teams.Where(t => t.CurrentLeague == league).ToList();
                
                return leagueTeams;
            }
            return null;
        }
    }
}
