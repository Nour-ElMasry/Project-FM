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
            var league = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);

            if (league == null) 
                return null;

            await _unitOfWork.TeamRepository.DeleteTeam(league);
            await _unitOfWork.Save();

            return league;
        }
    }
}
