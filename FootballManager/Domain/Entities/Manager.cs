namespace Domain.Entities;
public abstract class Manager
{
    public long ManagerId { get; set; }

    public long ManagerPersonId { get; set; }
    public Person ManagerPerson { get; set; }
    public long CurrentTeamId { get; set; }
    public Team CurrentTeam { get; set; }
}

