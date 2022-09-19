using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class DeleteTeamHandler : IRequestHandler<DeleteTeam, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(DeleteTeam request, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);

            if (team == null)
                return null;

            await _unitOfWork.TeamRepository.DeleteTeam(team);
            await _unitOfWork.Save();

            return team;
        }
    }
}
