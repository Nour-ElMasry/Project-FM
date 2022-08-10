namespace Domain.Entities.ManagerContainer;
public abstract class Manager
{
    public Person ManagerPerson { get; set; }
    public Team? CurrentTeam { get; set; }

    public Manager(Person managerPerson)
    {
        ManagerPerson = managerPerson;
    }
}

