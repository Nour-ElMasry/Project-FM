using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateMidfielderHandler : IRequestHandler<CreateMidfielder, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMidfielderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(CreateMidfielder request, CancellationToken cancellationToken)
        {
            var player = new Midfielder(request.PlayerPerson, request.Position, request.Stats);

            await _unitOfWork.PlayerRepository.AddPlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}
