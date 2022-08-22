using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetLeagueByIdHandler : IRequestHandler<GetLeagueById, League>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLeagueByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<League> Handle(GetLeagueById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);
        }
    }
}
