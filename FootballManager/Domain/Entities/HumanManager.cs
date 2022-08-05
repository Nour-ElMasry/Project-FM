namespace Domain.Entities;
public class HumanManager : User, IManager
{
    public HumanManager(string username, string password, string name, string birthDate, string nationality, string photo) : base(username, password, name, birthDate, nationality, photo)
    {
    }

    public Team? CurrentTeam { get; set; }

    public bool IsRobot() => false;
}

