namespace Domain.Entities;
public class FakeManager : Manager
{
    public FakeManager(Person managerPerson)
    {
        ManagerPerson = managerPerson;
    }
}

