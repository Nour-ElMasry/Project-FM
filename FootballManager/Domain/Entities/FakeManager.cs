namespace Domain.Entities;
public class FakeManager : Manager
{
    public FakeManager() { }
    public FakeManager(Person p)
    {
        ManagerPerson = p;
    }
}

