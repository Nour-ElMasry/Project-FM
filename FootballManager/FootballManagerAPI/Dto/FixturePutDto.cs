using System.ComponentModel.DataAnnotations;

namespace FootballManagerAPI.Dto
{
    public class FixturePutDto
    {
        [Required]
        [StringLength(10)]
        public string Date { get; set; }
    }
}
