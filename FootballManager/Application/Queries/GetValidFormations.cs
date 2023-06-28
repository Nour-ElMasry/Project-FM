using Domain.Entities;
using MediatR;

public class GetValidFormations : IRequest<List<Formation>>
{
    public long TeamId { get; set; }
}
