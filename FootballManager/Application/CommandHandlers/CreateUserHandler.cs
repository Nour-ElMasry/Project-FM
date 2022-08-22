using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = new User(request.Username, request.Password, request.UserPerson);

            await _unitOfWork.UserRepository.AddUser(user);
            await _unitOfWork.Save();

            return user;
        }
    }
}
