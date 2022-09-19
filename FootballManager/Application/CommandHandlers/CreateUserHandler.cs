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
            var uniqueCheck = await _unitOfWork.UserRepository.GetUserByName(request.Username) == null;

            if (uniqueCheck)
            {
                var person = new Person(request.Name, request.DateOfBirth, request.Country);

                if (request.Image != "")
                    person.Image = request.Image;

                var user = new User(request.Username, request.Password, person);

                await _unitOfWork.UserRepository.AddUser(user);
                await _unitOfWork.Save();

                return user;
            }

            return null;
        }
    }
}
