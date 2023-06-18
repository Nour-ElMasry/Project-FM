using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class RetireUserTeamHandler : IRequestHandler<RetireUserTeam, Team>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public RetireUserTeamHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Team> Handle(RetireUserTeam request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            var managers = await _unitOfWork.ManagerRepository.GetAllManagers();

            if (user == null)
                return null;

            var userManager = managers.SingleOrDefault(m => m.ManagerPerson == user.UserPerson);

            if (userManager == null)
                return null;

            var team = userManager.CurrentTeam;

            if (team == null)
                return null;

            var freeManager = managers.FindAll(m => m.CurrentTeam == null).FirstOrDefault();

            if (freeManager != null)
            {
                team.TeamManager = freeManager;
                freeManager.CurrentTeam = team;
            }

            team.TeamManager = null;

            await _unitOfWork.Save();

            return team;
        }
    }
}