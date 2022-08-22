using Application.Abstract;
using Application.Commands;
using MediatR;

namespace Application.CommandHandlers
{
    public class SimulateAFixtureHandler : IRequestHandler<SimulateAFixture>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SimulateAFixtureHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(SimulateAFixture request, CancellationToken cancellationToken)
        {
            var fixture = await _unitOfWork.FixtureRepository.GetFixtureById(request.FixtureID);
            
            if(fixture != null)
            {
                fixture.SimulateFixture();
                await _unitOfWork.Save();
            }

            return new Unit();
        }
    }
}
