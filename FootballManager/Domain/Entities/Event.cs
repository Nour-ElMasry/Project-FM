using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Event
    {
        [Key]
        public long EventId { get; set; }

        [ForeignKey("GoalScorerId")]
        public Player GoalScorer { get; set; }

        [ForeignKey("GoalAssisterId")]
        public Player GoalAssister { get; set; }

        [ForeignKey("EventFixtureId")]
        public Fixture EventFixture { get; set; }
    }
}
