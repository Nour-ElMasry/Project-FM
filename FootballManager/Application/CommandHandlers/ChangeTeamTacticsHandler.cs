using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class ChangeTeamTacticsHandler : IRequestHandler<ChangeTeamTactics, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeTeamTacticsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Team> Handle(ChangeTeamTactics request, CancellationToken cancellationToken)
        {
            Team team = await _unitOfWork.TeamRepository.GetTeamWithTactics(request.TeamId);

            if (team == null)
            {
                return null;
            }

            var flag = false;

            switch (request.Tactic.ToLower())
            {
                case "attacking":
                    team.CurrentTeamSheet.TeamTactic = new AttackingTactic();
                    break;
                case "balanced":
                    team.CurrentTeamSheet.TeamTactic = new BalancedTactic();
                    break;
                case "defending":
                    team.CurrentTeamSheet.TeamTactic = new DefendingTactic();
                    break;
                default:
                    flag = true;
                    break;
            }

            if (flag)
            {
                return null;
            }

            team.CurrentTeamSheet.UpdateRating();
            await _unitOfWork.Save();
            return team;
        }
    }
}
