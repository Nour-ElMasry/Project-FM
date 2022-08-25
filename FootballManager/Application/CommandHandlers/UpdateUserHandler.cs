using Application.Abstract;
using Application.Commands;
using MediatR;

namespace Application.CommandHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUser>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            await _unitOfWork.UserRepository.UpdateUser(request.UserId, request.UpdatedUser);
            await _unitOfWork.Save();

            return new Unit();
        }
    }
}
