using Application.Abstract;
using Application.Pagination;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetAllLeaguesForCampainHandler : IRequestHandler<GetAllLeaguesForCampain, List<League>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllLeaguesForCampainHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<League>> Handle(GetAllLeaguesForCampain request, CancellationToken cancellationToken)
        {
            return _unitOfWork.LeagueRepository.GetAllLeaguesWithTeamsForCampain();
        }
    }
}
