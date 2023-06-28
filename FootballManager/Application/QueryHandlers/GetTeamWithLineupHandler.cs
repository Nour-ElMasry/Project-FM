using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetTeamWithLineupHandler : IRequestHandler<GetTeamWithLineup, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTeamWithLineupHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(GetTeamWithLineup request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TeamRepository.GetTeamLineup(request.TeamId);
        }
    }
}
