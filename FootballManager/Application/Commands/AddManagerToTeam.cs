﻿using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class AddManagerToTeam : IRequest<Manager>
    {
        public long ManagerId { get; set; }
        public long TeamId { get; set; }
    }
}
