﻿using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateRealManagerHandler : IRequestHandler<CreateRealManager, RealManager>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRealManagerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RealManager> Handle(CreateRealManager request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(request.UserManagerId);

            if (user == null)
                return null;

            var manager = new RealManager(user);

            await _unitOfWork.ManagerRepository.AddManager(manager);
            await _unitOfWork.Save();

            return manager;
        }
    }
}
