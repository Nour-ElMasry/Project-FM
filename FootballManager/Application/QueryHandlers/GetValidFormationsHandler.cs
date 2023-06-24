using Domain.Entities;
using MediatR;

public class GetValidFormationsHandler : IRequestHandler<GetValidFormations, List<Formation>>
{
    public Task<List<Formation>> Handle(GetValidFormations request, CancellationToken cancellationToken)
    {
        List<Formation> validFormations = Formation.GetValidFormations();
        return Task.FromResult(validFormations);
    }
}
