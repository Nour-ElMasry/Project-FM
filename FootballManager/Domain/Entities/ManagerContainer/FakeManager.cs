using Domain.Entities.PersonContainer;

namespace Domain.Entities.ManagerContainer;
public class FakeManager : Manager
{
    public FakeManager(FakePerson managerPerson)
    {
        ManagerPerson = managerPerson;
    }
}

