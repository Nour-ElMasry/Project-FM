
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class DefaultTeam
    {
        [Key]
        public long DefaultTeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Venue { get; set; }
        public string Logo { get; set; } = "https://www.pngitem.com/pimgs/m/499-4991608_generic-football-club-logo-png-download-soccer-club.png";
        
        [ForeignKey("DefaultTeamManagerId")]
        public DefaultManager DefaultTeamManager { get; set; }

        [ForeignKey("DefaultTeamLeagueId")]
        public DefaultLeague DefaultTeamLeague { get; set; }
        public List<DefaultPlayer> Players { get; set; } = new();

        public DefaultTeam()
        {
        }

        public DefaultTeam(string name, string country, string venue, string logo)
        {
            Name = name;
            Country = country;
            Venue = venue;
            Logo = logo;
        }
    }
}