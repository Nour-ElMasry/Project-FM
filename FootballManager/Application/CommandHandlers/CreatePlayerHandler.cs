using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;
using System;

namespace Application.CommandHandlers
{
    public class CreatePlayerHandler : IRequestHandler<CreatePlayer, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePlayerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(CreatePlayer request, CancellationToken cancellationToken)
        {
            var role = request.Position.ToLower();
            var playerPerson = new Person(request.Name, request.DateOfBirth, request.Country);

            if (request.Image != "")
                playerPerson.Image = request.Image;

            switch (role)
            {
                case "attacker":
                    var attacker = new Attacker(playerPerson);

                    await _unitOfWork.PlayerRepository.AddPlayer(attacker);
                    await _unitOfWork.Save();

                    return attacker;

                case "midfielder":
                    var midfielder = new Midfielder(playerPerson);

                    await _unitOfWork.PlayerRepository.AddPlayer(midfielder);
                    await _unitOfWork.Save();

                    return midfielder;
                case "defender":
                    var defender = new Defender(playerPerson);

                    await _unitOfWork.PlayerRepository.AddPlayer(defender);
                    await _unitOfWork.Save();

                    return defender;
                case "goalkeeper":
                    var goalkeeper = new Goalkeeper(playerPerson);

                    await _unitOfWork.PlayerRepository.AddPlayer(goalkeeper);
                    await _unitOfWork.Save();

                    return goalkeeper;

                default:
                    throw new ArgumentException("Player role is incorrect!");
            }
        }
    }
}
