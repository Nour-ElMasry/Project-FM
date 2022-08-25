using Application.Abstract;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetPersonByIdHandler : IRequestHandler<GetPersonById, Person>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPersonByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Person> Handle(GetPersonById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.PeopleRepository.GetPersonById(request.PersonId);
        }
    }
}
