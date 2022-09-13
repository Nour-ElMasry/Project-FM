using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class DeleteLeagueHandler : IRequestHandler<DeleteLeague, League>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<League> Handle(DeleteLeague request, CancellationToken cancellationToken)
        {
            var league = await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);

            if (league == null) 
                return null;

            await _unitOfWork.LeagueRepository.DeleteLeague(league);
            await _unitOfWork.Save();

            return league;
        }
    }
}
