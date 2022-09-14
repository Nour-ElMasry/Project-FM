using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(request.UserId);

            if (user == null)
                return null;

            await _unitOfWork.UserRepository.DeleteUser(user);
            await _unitOfWork.Save();

            return user;
        }
    }
}
