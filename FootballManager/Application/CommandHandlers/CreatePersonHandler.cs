using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreatePersonHandler : IRequestHandler<CreatePerson, Person>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePersonHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Person> Handle(CreatePerson request, CancellationToken cancellationToken)
        {
            var person = new Person(request.Name, request.BirthDate, request.Country);

            await _unitOfWork.PeopleRepository.AddPerson(person);
            await _unitOfWork.Save();

            return person;
        }
    }
}
