using Application.Abstract;
using Application.Pagination;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetFixturesByTeamHandler : IRequestHandler<GetFixturesByTeam, List<Fixture>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFixturesByTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Fixture>> Handle(GetFixturesByTeam request, CancellationToken cancellationToken)
        {
            var fixtures = await _unitOfWork.FixtureRepository.GetAllFixturesByTeam(request.TeamId);

            if (fixtures == null)
                return null;

            return fixtures;
        }
    }
}
