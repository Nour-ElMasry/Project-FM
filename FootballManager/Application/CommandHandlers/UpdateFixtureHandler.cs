using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.CommandHandlers
{
    public class UpdateFixtureHandler : IRequestHandler<UpdateFixture, Fixture>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFixtureHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Fixture> Handle(UpdateFixture request, CancellationToken cancellationToken)
        {
            var fixture = await _unitOfWork.FixtureRepository.GetFixtureById(request.FixtureId);

            if (fixture != null)
            {
                if (!DateTime.TryParse(request.newDate, out DateTime tempDate))
                    throw new IncorrectDateException("Invalid date! please input a correct date!");

                if (DateTime.Now > tempDate)
                    throw new IncorrectDateException("Invalid date! please input a correct date!");

                fixture.Date = tempDate;

                await _unitOfWork.FixtureRepository.UpdateFixture(fixture);
                await _unitOfWork.Save();

                return fixture;
            }
            return null;
        }
    }
}