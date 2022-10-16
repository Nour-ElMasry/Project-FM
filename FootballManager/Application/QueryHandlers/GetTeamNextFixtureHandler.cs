using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetTeamNextFixtureHandler : IRequestHandler<GetTeamNextFixture, Fixture>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTeamNextFixtureHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Fixture> Handle(GetTeamNextFixture request, CancellationToken cancellationToken)
        {
            var fixture = await _unitOfWork.FixtureRepository.GetNextFixtureByTeam(request.TeamId);
           
            if (fixture == null)
                return null;

            return fixture;
        }
    }
}
