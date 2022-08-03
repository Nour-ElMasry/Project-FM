namespace Domain.Entities;
public abstract class Manager : Person
{
    public Manager(Team club, string name, string birthDate, string nationality, string photo) : base(name, birthDate, nationality, photo)
    {
        Club = club;
    }

    public Manager(Team club)
    {
        Club = club;
    }

    public Team Club { get; set; }

    public abstract bool IsRobot();
}

