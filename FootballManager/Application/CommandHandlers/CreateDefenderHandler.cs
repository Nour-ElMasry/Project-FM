using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateDefenderHandler : IRequestHandler<CreateDefender, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDefenderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(CreateDefender request, CancellationToken cancellationToken)
        {
            var player = new Defender(request.PlayerPerson, request.Position, request.Stats);

            await _unitOfWork.PlayerRepository.AddPlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}

