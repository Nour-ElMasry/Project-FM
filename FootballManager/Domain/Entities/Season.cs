namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;
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

