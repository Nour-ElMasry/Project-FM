using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, List<Player>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPlayersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Player>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.PlayerRepository.GetAllPlayers();
        }
    }
}
