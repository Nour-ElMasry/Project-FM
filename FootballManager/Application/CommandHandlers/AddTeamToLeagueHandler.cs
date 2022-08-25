using Application.Abstract;
using Application.Commands;
using MediatR;

namespace Application.CommandHandlers
{
    public class AddTeamToLeagueHandler : IRequestHandler<AddTeamToLeague>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTeamToLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddTeamToLeague request, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);
            var league = await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);

            if (league != null && team != null)
            {
                league.Teams.Add(team);
                team.CurrentLeague = league;
                await _unitOfWork.Save();
            }

            return new Unit();
        }
    }
}
