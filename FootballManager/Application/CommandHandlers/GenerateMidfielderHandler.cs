using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class GenerateMidfielderHandler : IRequestHandler<GenerateMidfielder, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateMidfielderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(GenerateMidfielder request, CancellationToken cancellationToken)
        {
            var player = new Midfielder(request.PlayerPerson, request.Position);

            await _unitOfWork.PlayerRepository.AddPlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}
