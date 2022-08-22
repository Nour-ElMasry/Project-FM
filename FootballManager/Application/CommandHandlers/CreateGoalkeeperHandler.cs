using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateGoalkeeperHandler : IRequestHandler<CreateGoalkeeper, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateGoalkeeperHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(CreateGoalkeeper request, CancellationToken cancellationToken)
        {
            var player = new Goalkeeper(request.PlayerPerson, "GK", request.Stats);

            await _unitOfWork.PlayerRepository.AddPlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}
