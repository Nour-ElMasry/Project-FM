using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class GenerateDefenderHandler : IRequestHandler<GenerateDefender, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateDefenderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(GenerateDefender request, CancellationToken cancellationToken)
        {
            var player = new Defender(request.PlayerPerson, request.Position);

            await _unitOfWork.PlayerRepository.AddPlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}
