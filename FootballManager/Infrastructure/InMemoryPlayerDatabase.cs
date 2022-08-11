
using Domain.Entities.PlayersContainer;

namespace Infrastructure;
public class InMemoryPlayerDatabase
{
    private static InMemoryPlayerDatabase? _database;
    private static readonly object _locker = new();
    private string file = Path.Combine(Directory.GetCurrentDirectory(), "Players.txt");
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

    public void PrintPlayers()
    {
        for (int i = 0; i < Players.Count; i++)
            Console.WriteLine($"{i + 1}:\n{Players[i]}");
    }

    public async void WriteToFile()
    {
        using var sw = new StreamWriter(file);
        for (int i = 0; i < Players.Count; i++)
            await sw.WriteLineAsync($"Player {i + 1} => \n {Players[i]}");
    }

    public void ReadPlayersFile()
    {
        using var sr = new StreamReader(file);
        Console.Write(sr.ReadToEnd());
    }
}
