using Application.Abstract;
using Application.Pagination;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, Pager<Player>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPlayersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pager<Player>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.PlayerRepository.GetAllPlayers(request.Page, request.Filter);
        }
    }
}
