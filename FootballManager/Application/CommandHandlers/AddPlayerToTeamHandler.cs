using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class AddPlayerToTeamHandler : IRequestHandler<AddPlayerToTeam, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddPlayerToTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(AddPlayerToTeam request, CancellationToken cancellationToken)
        {
            var player = await _unitOfWork.PlayerRepository.GetPlayerById(request.PlayerId);
            var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);

            if (player == null || team == null)
                return null;

            team.Players.Add(player);
            team.CurrentTeamSheet.UpdateRating(team.Players);

            await _unitOfWork.Save();

            return player;
        }
    }
}
