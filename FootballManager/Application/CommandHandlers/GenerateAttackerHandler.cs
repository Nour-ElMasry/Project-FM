using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class GenerateAttackerHandler : IRequestHandler<GenerateAttacker, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateAttackerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(GenerateAttacker request, CancellationToken cancellationToken)
        {
            var player = new Attacker(request.PlayerPerson, request.Position);

            await _unitOfWork.PlayerRepository.AddPlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}
