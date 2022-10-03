using Application.Abstract;
using Application.Pagination;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetFixturesByTeamHandler : IRequestHandler<GetFixturesByTeam, Pager<Fixture>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFixturesByTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pager<Fixture>> Handle(GetFixturesByTeam request, CancellationToken cancellationToken)
        {
            var fixtures = await _unitOfWork.FixtureRepository.GetAllFixturesByTeam(request.TeamId, request.Page);

            if (fixtures == null)
                return null;

            return fixtures;
        }
    }
}
