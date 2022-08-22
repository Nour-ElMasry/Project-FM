using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetManagerByIdHandler : IRequestHandler<GetManagerById, Manager>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetManagerByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Manager> Handle(GetManagerById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ManagerRepository.GetManagerById(request.ManagerId);
        }
    }
}
