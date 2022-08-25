using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.GetUserById(request.UserId);
        }
    }
}
