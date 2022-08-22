using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetManagerByTeamHandler : IRequestHandler<GetManagerByTeam, Manager>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetManagerByTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Manager> Handle(GetManagerByTeam request, CancellationToken cancellationToken)
        {
            var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);
            return team.TeamManager;
        }
    }
}
