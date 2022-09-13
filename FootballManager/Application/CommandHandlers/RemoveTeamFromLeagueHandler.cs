using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class RemoveTeamFromLeagueHandler : IRequestHandler<RemoveTeamFromLeague, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTeamFromLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(RemoveTeamFromLeague request, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);
            var league = await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);

            if (league == null || team == null)
                return null;

            league.RemoveTeam(team);
            await _unitOfWork.Save();

            return team;
        }
    }
}
