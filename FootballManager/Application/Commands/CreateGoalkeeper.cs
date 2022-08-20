using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class CreateGoalkeeper : IRequest<Goalkeeper>
{
    public Person PlayerPerson { get; set; }
}