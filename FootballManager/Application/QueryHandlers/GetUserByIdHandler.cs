﻿using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.QueryHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, User>
    {
        private readonly UserManager<User> _userManager;

        public GetUserByIdHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Include(u => u.UserPerson).FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null)
                return null;

            return user;
        }
    }
}
