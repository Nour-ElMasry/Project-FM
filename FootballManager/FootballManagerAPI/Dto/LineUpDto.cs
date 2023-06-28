using Application.Dto;

namespace FootballManagerAPI.Dto
{
    public class LineUpDto
    {
        public long TeamId { get; set; }
        public FormationDto Formation { get; set; }
        public List<LineUpPlayersDto> StartingEleven { get; set; }
        public List<LineUpPlayersDto> Bench { get; set; }
    }
}
