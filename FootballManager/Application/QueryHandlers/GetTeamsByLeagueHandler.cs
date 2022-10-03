using Application.Abstract;
using Application.Pagination;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetTeamsByLeagueHandler : IRequestHandler<GetTeamsByLeague, Pager<Team>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTeamsByLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pager<Team>> Handle(GetTeamsByLeague request, CancellationToken cancellationToken)
        {
            var teams = await _unitOfWork.TeamRepository.GetTeamsByLeagueId(request.LeagueId, request.Page);

            return teams;
        }
    }
}
