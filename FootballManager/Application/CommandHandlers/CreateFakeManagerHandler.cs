using Application.Abstract;
using Application.Commands;
using Domain.Entities;
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
            var manager = new FakeManager(request.ManagerPersonId);

            await _unitOfWork.ManagerRepository.AddManager(manager);
            await _unitOfWork.Save();

            return manager;
        }
    }
}
