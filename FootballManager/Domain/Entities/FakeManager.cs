namespace Domain.Entities;
public class FakeManager : Manager
{
    public FakeManager(FakePerson managerPerson)
    {
        ManagerPerson = managerPerson;
    }
}

