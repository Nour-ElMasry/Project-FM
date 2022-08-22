using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateRealManagerHandler : IRequestHandler<CreateRealManager, Manager>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRealManagerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Manager> Handle(CreateRealManager request, CancellationToken cancellationToken)
        {
            var manager = new RealManager(request.UserManager);

            await _unitOfWork.ManagerRepository.AddManager(manager);
            await _unitOfWork.Save();

            return manager;
        }
    }
}
