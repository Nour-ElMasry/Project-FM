using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateMidfielderHandler : IRequestHandler<CreateMidfielder, Midfielder>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMidfielderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Midfielder> Handle(CreateMidfielder request, CancellationToken cancellationToken)
        {
            var player = new Midfielder(request.PlayerPersonId, request.Position);

            await _unitOfWork.PlayerRepository.AddPlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}
