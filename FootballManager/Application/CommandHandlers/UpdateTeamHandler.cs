using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class UpdateTeamHandler : IRequestHandler<UpdateTeam, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(UpdateTeam request, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);

            if (team != null)
            {
                team.Name = request.Name;
                team.Country = request.Country;
                team.Venue = request.Venue;

                await _unitOfWork.TeamRepository.UpdateTeam(team);
                await _unitOfWork.Save();

                return team;
            }
            return null;
        }
    }
}