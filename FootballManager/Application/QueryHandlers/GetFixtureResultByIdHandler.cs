using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetFixtureResultByIdHandler : IRequestHandler<GetFixtureResultById, Fixture>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFixtureResultByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Fixture> Handle(GetFixtureResultById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.FixtureRepository.GetFixtureResultById(request.FixtureId);
        }
    }
}
