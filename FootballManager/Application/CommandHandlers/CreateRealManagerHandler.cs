using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandHandlers
{
    public class CreateRealManagerHandler : IRequestHandler<CreateRealManager, RealManager>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public CreateRealManagerHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<RealManager> Handle(CreateRealManager request, CancellationToken cancellationToken)
        {
            var managers = await _unitOfWork.ManagerRepository.GetAllManagers();
            var user = await _userManager.Users.Include(u => u.UserPerson).FirstOrDefaultAsync(u => u.Id == request.UserManagerId);

            if (user == null)
                return null;

            var check = managers.SingleOrDefault(m => m.ManagerPerson.PersonId == user.UserPerson.PersonId);

            if (check != null)
                return (RealManager)check;

            var manager = new RealManager(user);

            await _unitOfWork.ManagerRepository.AddManager(manager);
            await _unitOfWork.Save();

            return manager;
        }
    }
}
