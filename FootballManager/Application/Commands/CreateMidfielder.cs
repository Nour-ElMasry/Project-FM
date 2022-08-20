﻿using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class CreateMidfielder : IRequest<Midfielder>
{
    public Person PlayerPerson { get; set; }
    public string Position { get; set; }
}