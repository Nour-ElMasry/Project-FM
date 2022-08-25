using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class AuthUserHandler : IRequestHandler<AuthUser, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(AuthUser request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByName(request.UserName);

            var isRightPassword = user.Password == request.Password;

            if (isRightPassword)
                return user;

            return null;
        }
    }
}
