using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetAllLeaguesHandler : IRequestHandler<GetAllLeagues, List<League>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllLeaguesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<League>> Handle(GetAllLeagues request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.LeagueRepository.GetAllLeagues();
        }
    }
}
