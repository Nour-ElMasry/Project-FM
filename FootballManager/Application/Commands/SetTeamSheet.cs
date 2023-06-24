using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class SetTeamSheet : IRequest<Team>
{
    public long TeamId { get; set; }
}
