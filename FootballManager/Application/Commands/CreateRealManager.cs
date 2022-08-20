using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateRealManager : IRequest<RealManager>
    {
        public User UserManager { get; set; }
    }
}
