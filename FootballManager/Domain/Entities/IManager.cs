namespace Domain.Entities;
public interface IManager 
{
    bool IsRobot();
    public Team? CurrentTeam { get; set; }
}

