using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class GenerateGoalkeeperHandler : IRequestHandler<GenerateGoalkeeper, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateGoalkeeperHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(GenerateGoalkeeper request, CancellationToken cancellationToken)
        {
            var player = new Goalkeeper(request.PlayerPerson, "GK");

            await _unitOfWork.PlayerRepository.AddPlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}
