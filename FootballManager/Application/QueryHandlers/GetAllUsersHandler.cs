using Application.Pagination;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.QueryHandlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsers, Pager<User>>
    {
        private readonly UserManager<User> _userManager;

        public GetAllUsersHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Pager<User>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            var page = new Pager<User>(await _userManager.Users.CountAsync(), request.Page);

            if (request.Page == 0)
            {
                page.PageResults = await _userManager.Users.Include(u => u.UserPerson).ToListAsync();

                return page;
            }

            page.PageResults = await _userManager.Users.Include(u => u.UserPerson)
                .Skip((request.Page - 1) * 10)
                .Take(10)
                .ToListAsync();

            return page;
        }
    }
}
