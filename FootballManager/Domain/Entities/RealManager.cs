namespace Domain.Entities;
public class RealManager : Manager
{
    public User UserManager { get; set; }
    public RealManager() { }
    public RealManager(User u)
    {
        UserManager = u ?? throw new ArgumentNullException(nameof(u));
        ManagerPersonId = UserManager.UserPersonId;
    }
}

