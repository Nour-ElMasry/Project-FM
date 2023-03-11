using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class DefaultLeague
    {
        [Key]
        public long DefaultLeagueId { get; set; }
        public string Name { get; set; }
        public List<DefaultTeam> Teams { get; set; } = new();

        public DefaultLeague() { }
        public DefaultLeague(string name)
        {
            Name = name;
        }
    }
}
