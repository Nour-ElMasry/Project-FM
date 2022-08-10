using Domain.Entities.PersonContainer;
using Domain.Entities.TeamContainer;

namespace Domain.Entities.ManagerContainer;
public abstract class Manager
{
    public Person? ManagerPerson { get; set; }
    public Team? CurrentTeam { get; set; }
}

