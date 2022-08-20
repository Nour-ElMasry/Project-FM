﻿using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreatePerson : IRequest<Person>
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
    }
}
