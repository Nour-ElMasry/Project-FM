using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.CommandHandlers
{
    public class NextSeasonHandler : IRequestHandler<NextSeason, League>
    {
        private readonly IUnitOfWork _unitOfWork;

        public NextSeasonHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<League> Handle(NextSeason request, CancellationToken cancellationToken)
        {
            var league = await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);

            if (league == null)
                return null;

            league.NextSeason();
            await _unitOfWork.Save();
            
            return league;
        }
    }
}