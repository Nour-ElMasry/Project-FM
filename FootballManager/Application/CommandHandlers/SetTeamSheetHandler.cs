using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers;

public class SetTeamSheetHandler : IRequestHandler<SetTeamSheet, Team>
{
    private readonly IUnitOfWork _unitOfWork;

    public SetTeamSheetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Team> Handle(SetTeamSheet request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.TeamRepository.GetTeamById(request.TeamId);
        if (team == null)
        {
            return null;
        }

        var teamPlayers = await _unitOfWork.PlayerRepository.GetAllPlayersByTeam(request.TeamId, 0 , null);

        team.CurrentTeamSheet.SetFormationPlayers(teamPlayers.PageResults);

        await _unitOfWork.Save();

        return team;
    }
}
