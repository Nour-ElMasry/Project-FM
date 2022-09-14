using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetFixturesByLeagueHandler : IRequestHandler<GetFixturesByLeague, List<Fixture>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFixturesByLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Fixture>> Handle(GetFixturesByLeague request, CancellationToken cancellationToken)
        {
            var fixtures = await _unitOfWork.FixtureRepository.GetAllFixtures();

            if (fixtures == null)
                return null;

            return fixtures.Where(f => f.FixtureLeague.LeagueId == request.LeagueId).ToList();
        }
    }
}
