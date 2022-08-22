﻿using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateFakeManagerHandler : IRequestHandler<CreateFakeManager, Manager>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateFakeManagerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Manager> Handle(CreateFakeManager request, CancellationToken cancellationToken)
        {
            var manager = new FakeManager(request.ManagerPerson);

            await _unitOfWork.ManagerRepository.AddManager(manager);
            await _unitOfWork.Save();

            return manager;
        }
    }
}