namespace Domain.Entities;
public class HumanManager : Manager
{
    public HumanManager(Team club, string name, string birthDate, string nationality, string photo) : base(club, name, birthDate, nationality, photo)
    {
    }

    public override bool IsRobot() => false;

}
