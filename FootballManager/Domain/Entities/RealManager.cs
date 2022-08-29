using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class RealManager : Manager
{
    [ForeignKey("UserManagerId")]
    public User UserManager { get; set; }
    public RealManager() { }
    public RealManager(User u)
    {
        UserManager = u ?? throw new ArgumentNullException(nameof(u));
        ManagerPerson = UserManager.UserPerson;
    }
}

