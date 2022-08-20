using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateTeam : IRequest<Team>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Venue { get; set; }
        public Manager TeamManager { get; set; }
    }
}
