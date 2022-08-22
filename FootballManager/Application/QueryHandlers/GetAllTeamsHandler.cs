using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetAllTeamsHandler : IRequestHandler<GetAllTeams, List<Team>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTeamsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Team>> Handle(GetAllTeams request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TeamRepository.GetAllTeams();
        }
    }
}
