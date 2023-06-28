using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class ChangeTeamFormationHandler : IRequestHandler<ChangeTeamFormation, Formation>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeTeamFormationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Formation> Handle(ChangeTeamFormation request, CancellationToken cancellationToken)
        {
            Team team = await _unitOfWork.TeamRepository.GetTeamLineup(request.TeamId);

            if (team == null)
            {
                return null;
            }

            var players = await _unitOfWork.PlayerRepository.GetAllPlayersByTeam(team.TeamId, 0, null);

            if (players == null)
            {
                return null;
            }

            team.CurrentTeamSheet.TeamFormation = new Formation(request.FormationDefenders, request.FormationMidfielders, request.FormationAttackers);
            team.CurrentTeamSheet.SetFormationPlayers(players.PageResults);
            await _unitOfWork.Save();
            return team.CurrentTeamSheet.TeamFormation;
        }
    }
}
