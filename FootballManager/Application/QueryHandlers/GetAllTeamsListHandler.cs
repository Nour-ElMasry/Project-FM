using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetAllTeamsListHandler : IRequestHandler<GetAllTeamsList, List<Team>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTeamsListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Team>> Handle(GetAllTeamsList request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TeamRepository.GetTeamsList(); 
        }
    }
}