using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class DefaultPlayer
    {
        [Key]
        public long PlayerId { get; set; }

        [ForeignKey("PlayerPersonId")]
        public Person PlayerPerson { get; set; }
        public string Position { get; set; }

        [ForeignKey("DefaultPlayerTeamId")]
        public DefaultTeam DefaultPlayerTeam { get; set; }

        [ForeignKey("DefaultPlayerStatsId")]
        public PlayerStats DefaultPlayerStats { get; set; }


        public DefaultPlayer() { }

        public DefaultPlayer(Person person, string position)
        {
            PlayerPerson = person;
            Position = position;
            DefaultPlayerStats = PlayerStatsFactory.GenerateStats(position);
        }
    }
}
