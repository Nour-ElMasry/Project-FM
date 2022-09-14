using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class SimulateAFixtureHandler : IRequestHandler<SimulateAFixture, Fixture>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SimulateAFixtureHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Fixture> Handle(SimulateAFixture request, CancellationToken cancellationToken)
        {
            var fixture = await _unitOfWork.FixtureRepository.GetFixtureById(request.FixtureID);

            if (fixture != null)
            {
                if (fixture.FixtureLeague.LeagueId != request.LeagueId)
                    throw new ArgumentException("Fixture doesn't belong to current league!");

                fixture.SimulateFixture();
                await _unitOfWork.Save();

                return fixture;
            }

            return null;
        }
    }
}
