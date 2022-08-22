using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class DeletePlayerHandler : IRequestHandler<DeletePlayer, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePlayerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(DeletePlayer request, CancellationToken cancellationToken)
        {
            var player = await _unitOfWork.PlayerRepository.GetPlayerById(request.PlayerId);
            if (player == null) return null;

            await _unitOfWork.PlayerRepository.DeletePlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}
