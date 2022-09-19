using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetNumberOfTeams : IRequest<int>
    {
        public long LeagueId { get; set; }
    }
}
