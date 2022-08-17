namespace Domain.Entities;
public abstract class Manager
{
    public Person? ManagerPerson { get; set; }
    public Team? CurrentTeam { get; set; }
}

