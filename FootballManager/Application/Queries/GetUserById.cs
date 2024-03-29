﻿using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetUserById : IRequest<User>
    {
        public string UserId { get; set; }
    }
}
