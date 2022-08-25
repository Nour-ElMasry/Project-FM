using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetUserByUserNameHandler : IRequestHandler<GetUserByUserName, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByUserNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(GetUserByUserName request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.GetUserByName(request.UserName);
        }
    }
}
