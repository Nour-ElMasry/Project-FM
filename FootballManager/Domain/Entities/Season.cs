using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Season
{
    [Key]
    public long SeasonId { get; set; }
    public int Year { get; set; }

    public Season() { }
    public Season(int year)
    {
        Year = year;
    }
}

