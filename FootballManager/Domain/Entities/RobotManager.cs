namespace Domain.Entities;
public class RobotManager : Person, IManager
{
    public RobotManager(string name, string birthDate, string nationality, string photo) : base(name, birthDate, nationality, photo)
    {

    }
    public Team? CurrentTeam { get; set; }

    public bool IsRobot() => true;
}

