using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class ChangeTeamFormation : IRequest<Formation>
{
    public long TeamId { get; set; }
    public int FormationAttackers { get; set; }
    public int FormationMidfielders { get; set; }
    public int FormationDefenders { get; set; }
}
