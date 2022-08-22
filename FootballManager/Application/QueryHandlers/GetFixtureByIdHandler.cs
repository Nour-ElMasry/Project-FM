using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetFixtureByIdHandler : IRequestHandler<GetFixtureById, Fixture>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFixtureByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Fixture> Handle(GetFixtureById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.FixtureRepository.GetFixtureById(request.FixtureId);
        }
    }
}
