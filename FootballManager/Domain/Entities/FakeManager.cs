namespace Domain.Entities;
public class FakeManager : Manager
{
    public FakeManager() { }
    public FakeManager(long personId)
    {
        ManagerPersonId = personId;
    }
}

