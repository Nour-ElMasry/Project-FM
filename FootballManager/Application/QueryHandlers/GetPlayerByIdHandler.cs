using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetPlayerByIdHandler : IRequestHandler<GetPlayerById, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPlayerByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(GetPlayerById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.PlayerRepository.GetPlayerById(request.PlayerId);
        }
    }
}
