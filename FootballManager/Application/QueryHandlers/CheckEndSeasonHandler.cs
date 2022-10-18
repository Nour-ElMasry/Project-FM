using Application.Abstract;
using Application.Queries;
using MediatR;

namespace Application.QueryHandlers
{
    public class CheckEndSeasonHandler : IRequestHandler<CheckEndSeason, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckEndSeasonHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CheckEndSeason request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _unitOfWork.FixtureRepository.EndOfSeasonCheck());
        }
    }
}
