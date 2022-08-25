﻿using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateDefenderHandler : IRequestHandler<CreateDefender, Defender>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDefenderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Defender> Handle(CreateDefender request, CancellationToken cancellationToken)
        {
            var player = new Defender(request.PlayerPersonId, request.Position);

            await _unitOfWork.PlayerRepository.AddPlayer(player);
            await _unitOfWork.Save();

            return player;
        }
    }
}

