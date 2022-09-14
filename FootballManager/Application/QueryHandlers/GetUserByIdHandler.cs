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
            var user = await _unitOfWork.UserRepository.GetUserById(request.UserId);

            if (user == null)
                return null;

            return user;
        }
    }
}
