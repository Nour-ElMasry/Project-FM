using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Score
    {
        [Key]
        public long ScoreId { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
