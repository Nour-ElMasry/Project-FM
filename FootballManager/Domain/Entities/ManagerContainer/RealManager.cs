using Domain.Entities.PersonContainer;
using Domain.Entities.UserContianer;

namespace Domain.Entities.ManagerContainer;
public class RealManager : Manager
{
    public User UserManager { get; set; }
    public RealManager(User u)
    {
        UserManager = u ?? throw new ArgumentNullException(nameof(u));
        ManagerPerson = UserManager.UserPerson;
    }       
}

