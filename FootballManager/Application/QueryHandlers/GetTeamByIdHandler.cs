using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetTeamByIdHandler : IRequestHandler<GetTeamById, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTeamByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(GetTeamById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);
        }
    }
}
