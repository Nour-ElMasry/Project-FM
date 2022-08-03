namespace Domain.Entities;
public class RobotManager : Manager
{
    public RobotManager(Team club, string name, string birthDate, string nationality, string photo) : base(club, name, birthDate, nationality, photo)
    {
    }

    public override bool IsRobot() => true;
}

