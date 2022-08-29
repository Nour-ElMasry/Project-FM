using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateGoalkeeperHandler : IRequestHandler<CreateGoalkeeper, Goalkeeper>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateGoalkeeperHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Goalkeeper> Handle(CreateGoalkeeper request, CancellationToken cancellationToken)
        {
            var player = new Goalkeeper(request.PlayerPerson, "GK");

            await _unitOfWork.PlayerRepository.AddPlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}
