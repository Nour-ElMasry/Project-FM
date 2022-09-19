using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
