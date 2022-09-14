using System.ComponentModel.DataAnnotations;

namespace FootballManagerAPI.Dto
{
    public class TeamPutPostDto
    {
        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        public string Country { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(4)]
        public string Venue { get; set; }

        [MaxLength(100)]
        public string Logo { get; set; }
    }
}
