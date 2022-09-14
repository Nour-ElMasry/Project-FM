using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateFakeManagerHandler : IRequestHandler<CreateFakeManager, FakeManager>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateFakeManagerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FakeManager> Handle(CreateFakeManager request, CancellationToken cancellationToken)
        {
            if (!DateTime.TryParse(request.DateOfBirth, out DateTime tempDate))
                throw new IncorrectDateException("Invalid date! please input a correct date!");

            if (DateTime.Now.Year - 18 <= tempDate.Year)
                throw new IncorrectDateException("Invalid date! Manager can't be under 18!");

            var person = new Person(request.Name, request.DateOfBirth, request.Country);

            var manager = new FakeManager(person);

            await _unitOfWork.ManagerRepository.AddManager(manager);
            await _unitOfWork.Save();

            return manager;
        }
    }
}
