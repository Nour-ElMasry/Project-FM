using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.CommandHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUser, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(request.UserId);

            if (user == null)
                return null;

            if (!DateTime.TryParse(request.DateOfBirth, out DateTime tempDate))
                throw new IncorrectDateException("Invalid date! please input a correct date!");

            if (DateTime.Now.Year - 16 <= tempDate.Year)
                throw new IncorrectDateException("Invalid date! Player can't be under 16!");

            user.UserPerson.Name = request.Name;
            user.UserPerson.Country = request.Country;
            user.UserPerson.BirthDate = tempDate;
            user.UserPerson.Image = request.Image;

            await _unitOfWork.UserRepository.UpdateUser(user);
            await _unitOfWork.Save();

            return user;
        }
    }
}
