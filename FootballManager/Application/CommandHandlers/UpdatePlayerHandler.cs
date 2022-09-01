using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.CommandHandlers
{
    public class UpdatePlayerHandler : IRequestHandler<UpdatePlayer, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePlayerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(UpdatePlayer request, CancellationToken cancellationToken)
        {
            var player = await _unitOfWork.PlayerRepository.GetPlayerById(request.PlayerId);

            if (player != null)
            {
                if (!PlayerPositions.IsCorrectPosition(request.Position, player.GetType().Name))
                    throw new IncorrectPositionException("Incorrect position assigned to role!");

                if (!DateTime.TryParse(request.DateOfBirth, out DateTime tempDate))
                    throw new IncorrectDateException("Invalid date! please input a correct date!");

                if (DateTime.Now.Year - 16 <= tempDate.Year)
                    throw new IncorrectDateException("Invalid date! Player can't be under 16!");

                player.PlayerPerson.Name = request.Name;
                player.PlayerPerson.Country = request.Country;
                player.PlayerPerson.BirthDate = tempDate;
                player.Position = request.Position;

                await _unitOfWork.PlayerRepository.UpdatePlayer(player);
                await _unitOfWork.Save();

                return player;
            }
            return null;
        }
    }
}