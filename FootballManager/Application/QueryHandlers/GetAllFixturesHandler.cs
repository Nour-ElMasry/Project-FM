using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetAllFixturesHandler : IRequestHandler<GetAllFixtures, List<Fixture>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllFixturesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Fixture>> Handle(GetAllFixtures request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.FixtureRepository.GetAllFixtures();
        }
    }
}
