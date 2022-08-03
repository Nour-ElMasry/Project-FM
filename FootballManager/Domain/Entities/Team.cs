
namespace Domain.Entities;
public class Team
{
    private static int _id = 0;
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Venue { get; set; }

    public Team(string name, string country, string venue)
    {
        Id = _id++;
        Name = name;
        Country = country;
        Venue = venue;
    }
}
