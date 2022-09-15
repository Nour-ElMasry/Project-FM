using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetNumberOfTeamsHandler : IRequestHandler<GetNumberOfTeams, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNumberOfTeamsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetNumberOfTeams request, CancellationToken cancellationToken)
        {
            var num = await _unitOfWork.TeamRepository.GetNumberOfTeams();
            
            return num;
        }
    }
}
