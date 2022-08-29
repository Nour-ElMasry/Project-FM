using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateTeamHandler : IRequestHandler<CreateTeam, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(CreateTeam request, CancellationToken cancellationToken)
        {
            var team = new Team(request.Name, request.Country, request.Venue);
            
   
            await _unitOfWork.TeamRepository.AddTeam(team);
            await _unitOfWork.Save();

            return team;
        }
    }
}
