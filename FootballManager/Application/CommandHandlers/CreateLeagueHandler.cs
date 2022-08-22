using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateLeagueHandler : IRequestHandler<CreateLeague, League>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLeagueHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<League> Handle(CreateLeague request, CancellationToken cancellationToken)
        {
            var league = new League(request.Name);

            await _unitOfWork.LeagueRepository.AddLeague(league);
            await _unitOfWork.Save();

            return league;
        }
    }
}
