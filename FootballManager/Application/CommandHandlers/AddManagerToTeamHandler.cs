using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class AddManagerToTeamHandler : IRequestHandler<AddManagerToTeam, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddManagerToTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(AddManagerToTeam request, CancellationToken cancellationToken)
        {
            var manager = await _unitOfWork.ManagerRepository.GetManagerById(request.ManagerId);
            var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);

            if (manager == null || team == null)
                return null;

            team.TeamManager = manager;
            manager.CurrentTeam = team;

            await _unitOfWork.Save();
            return team;
        }
    }
}
