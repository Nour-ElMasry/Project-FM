using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class RemovePlayerFromTeamHandler : IRequestHandler<RemovePlayerFromTeam, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemovePlayerFromTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(RemovePlayerFromTeam request, CancellationToken cancellationToken)
        {
            var player = await _unitOfWork.PlayerRepository.GetPlayerById(request.PlayerId);
            var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);

            if (player == null || team == null)
                return null;

            var playerToRemove = team.Players.First(p => p.PlayerId == player.PlayerId);

            if (playerToRemove == null)
                throw new NullReferenceException("Player doesn't exist in this team!");

            team.Players.Remove(playerToRemove);

            await _unitOfWork.Save();


            return playerToRemove;
        }
    }
}
