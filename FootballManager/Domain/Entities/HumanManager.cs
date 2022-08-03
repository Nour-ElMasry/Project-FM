namespace Domain.Entities;
public class HumanManager : Manager
{
    public HumanManager(User user, Team club) : base(club)
    {
        CurrentUser = user ?? throw new ArgumentNullException(nameof(user));
        Name = user.Name;
        BirthDate = user.BirthDate;
        Nationality = user.Nationality;
        Photo = user.Photo;
    }

    public User CurrentUser { get; set; }
    public override bool IsRobot() => false;
}
