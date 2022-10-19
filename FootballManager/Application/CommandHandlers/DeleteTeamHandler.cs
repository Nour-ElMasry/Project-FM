using Application.Abstract;
using Application.Commands;
using Application.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class DeleteTeamHandler : IRequestHandler<DeleteTeam, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(DeleteTeam request, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);

            if (team == null)
                return null;

            var availableTeams = await _unitOfWork.TeamRepository.GetTeamsNotAssignedToLeagues();

            var rand = new Random();
            var replacementTeam = availableTeams[rand.Next(availableTeams.Count - 1)];
            replacementTeam.Players = ((Pager<Player>)await _unitOfWork.PlayerRepository.GetAllPlayersByTeam(team.TeamId, 0, null)).PageResults;
            replacementTeam.CurrentTeamSheet.UpdateRating(replacementTeam.Players);

            replacementTeam.CurrentLeague = team.CurrentLeague;

            await _unitOfWork.FixtureRepository.ClearLeagueFixtures(replacementTeam.CurrentLeague.LeagueId);
            await _unitOfWork.TeamRepository.DeleteTeam(team);
            await _unitOfWork.Save();

            return team;
        }
    }
}
