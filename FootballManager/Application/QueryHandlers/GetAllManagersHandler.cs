using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetAllManagersHandler : IRequestHandler<GetAllManagers, List<Manager>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllManagersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Manager>> Handle(GetAllManagers request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ManagerRepository.GetAllManagers();
        }
    }
}
