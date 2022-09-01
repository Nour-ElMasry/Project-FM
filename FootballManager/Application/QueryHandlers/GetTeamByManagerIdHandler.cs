using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetTeamByManagerIdHandler : IRequestHandler<GetTeamByManagerId, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTeamByManagerIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(GetTeamByManagerId request, CancellationToken cancellationToken)
        {
            var manager = await _unitOfWork.ManagerRepository.GetManagerById(request.ManagerId);

            if (manager == null)
                return null;

            return await _unitOfWork.TeamRepository.GetTeamById(manager.CurrentTeam.TeamId);
        }
    }
}
