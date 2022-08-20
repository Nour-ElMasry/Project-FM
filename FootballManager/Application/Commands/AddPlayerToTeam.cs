using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AddPlayerToTeam : IRequest
    {
        public long PlayerId { get; set; }
        public long TeamId { get; set; }
    }
}
