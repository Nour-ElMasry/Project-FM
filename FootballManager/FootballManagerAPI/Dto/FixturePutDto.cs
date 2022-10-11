using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class FixturePutDto
    {
        [Required]
        [StringLength(10)]
        public string Date { get; set; }
    }
}
