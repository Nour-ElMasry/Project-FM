using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DefaultManager : Manager
    {
        public DefaultManager() { }
        public DefaultManager(Person p)
        {
            ManagerPerson = p;
        }
    }
}
