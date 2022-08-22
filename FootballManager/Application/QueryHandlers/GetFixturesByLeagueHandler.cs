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
            var league = await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);
            return league.Fixtures;
        }
    }
}
