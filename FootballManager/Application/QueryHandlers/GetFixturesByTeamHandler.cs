using Application.Abstract;
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
            var fixtures = await _unitOfWork.FixtureRepository.GetAllFixtures();
            
            if (fixtures == null)
                return null;

            var teamFixtures = fixtures.Where(f => f.HomeTeam.TeamId == request.TeamId || f.AwayTeam.TeamId == request.TeamId).ToList();

            return teamFixtures;
        }
    }
}
