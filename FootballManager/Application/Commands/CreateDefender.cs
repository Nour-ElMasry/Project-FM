﻿using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateDefender : IRequest<Defender>
    {
        public Person PlayerPerson { get; set; }
        public string Position { get; set; }
    }
}
