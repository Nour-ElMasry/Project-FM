using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AddTeamToLeague : IRequest
    {
        public long TeamId { get; set; }
        public long LeagueId { get; set; }
    }
}
