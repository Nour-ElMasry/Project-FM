using Application.Abstract;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class GetValidFormationsHandler : IRequestHandler<GetValidFormations, List<Formation>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetValidFormationsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Formation>> Handle(GetValidFormations request, CancellationToken cancellationToken)
    {
        Team team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);

        if (team == null)
        {
            return null;
        }

        var players = await _unitOfWork.PlayerRepository.GetAllPlayersByTeam(team.TeamId, 0, null);

        if (players == null)
        {
            return null;
        }

        var defenders = players.PageResults.Where(p => p.GetType().Name == "Defender").Count();
        var midfielders = players.PageResults.Where(p => p.GetType().Name == "Midfielder").Count();
        var attackers = players.PageResults.Where(p => p.GetType().Name == "Attacker").Count();


        List<Formation> validFormations = Formation.GetValidFormations(defenders, midfielders, attackers);

        return validFormations;
    }
}
