namespace Domain.Entities;
public class RealManager : Manager
{
    public RealManager() { }
    public RealManager(User u)
    {
        if (u is null)
            throw new ArgumentNullException(nameof(u));

        ManagerPerson = u.UserPerson;
    }
}
