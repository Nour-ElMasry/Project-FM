using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.QueryHandlers
{
    public class GetTeamByUserIdHandler : IRequestHandler<GetTeamByUserId, Team>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public GetTeamByUserIdHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Team> Handle(GetTeamByUserId request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            var managers = await _unitOfWork.ManagerRepository.GetAllManagers();

            if (user == null)
                return null;

            var userManager = managers.SingleOrDefault(m => m.ManagerPerson == user.UserPerson);

            if (userManager == null)
                return null;

            var team = userManager.CurrentTeam;

            if (team == null)
                return null;

            return await _unitOfWork.TeamRepository.GetTeamById(team.TeamId);
        }
    }
}
