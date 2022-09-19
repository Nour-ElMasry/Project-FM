using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetTeamByUserIdHandler : IRequestHandler<GetTeamByUserId, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTeamByUserIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(GetTeamByUserId request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(request.UserId);
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
