using Application.Dto;
using Domain.Entities;

namespace FootballManagerAPI.Dto
{
    public class LineUpPlayersDto
    {
        public long Id { get; set; }
        public Person PlayerPerson { get; set; }
        public PlayerStats PlayerStats { get; set; }
        public string Position { get; set; }
    }
}
