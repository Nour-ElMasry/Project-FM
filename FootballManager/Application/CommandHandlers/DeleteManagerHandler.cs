using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class DeleteManagerHandler : IRequestHandler<DeleteManager, Manager>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteManagerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Manager> Handle(DeleteManager request, CancellationToken cancellationToken)
        {
            var manager = await _unitOfWork.ManagerRepository.GetManagerById(request.ManagerId);
            if (manager == null) return null;

            await _unitOfWork.ManagerRepository.DeleteManager(manager);
            await _unitOfWork.Save();

            return manager;
        }
    }
}
