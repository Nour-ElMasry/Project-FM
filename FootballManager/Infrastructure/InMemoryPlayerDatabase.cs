
using Domain.Entities.PlayersContainer;

namespace Infrastructure;
public class InMemoryPlayerDatabase
{
    private static InMemoryPlayerDatabase? _database;
    private static readonly object _locker = new();

    public List<Player> Players { get; set; }
    private InMemoryPlayerDatabase()
    {
        Players = new();
    }

    public static InMemoryPlayerDatabase GetInstance()
    {
        lock (_locker)
        {
            if (_database == null)
                _database = new InMemoryPlayerDatabase();
        }

        return _database;
    }

    public void SavePlayer(Player p)
    {
       Players.Add(p);
    }

    public void RemovePlayer(Player p)
    {
       Players.Remove(p);
    }

}
