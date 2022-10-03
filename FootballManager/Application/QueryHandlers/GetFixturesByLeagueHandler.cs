using Application.Abstract;
using Application.Pagination;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetFixturesByLeagueHandler : IRequestHandler<GetFixturesByLeague, Pager<Fixture>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFixturesByLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pager<Fixture>> Handle(GetFixturesByLeague request, CancellationToken cancellationToken)
        {
            var fixtures = await _unitOfWork.FixtureRepository.GetAllFixturesByLeague(request.LeagueId, request.Page);

            if (fixtures == null)
                return null;

            return fixtures;
        }
    }
}
