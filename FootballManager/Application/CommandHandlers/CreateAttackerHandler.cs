using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateAttackerHandler : IRequestHandler<CreateAttacker, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAttackerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(CreateAttacker request, CancellationToken cancellationToken)
        {
            var player = new Attacker(request.PlayerPerson, request.Position, request.Stats);

            await _unitOfWork.PlayerRepository.AddPlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}
