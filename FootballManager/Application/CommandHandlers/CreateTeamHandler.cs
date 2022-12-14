using Application.Abstract;
using Application.Commands;
using Domain.Entities;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateTeamHandler : IRequestHandler<CreateTeam, Team>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> Handle(CreateTeam request, CancellationToken cancellationToken)
        {
            var team = new Team(request.Name, request.Country, request.Venue);

            if (request.Logo != "" && request.Logo != null)
                team.Logo = request.Logo;

            var league = await _unitOfWork.LeagueRepository.GetLeagueById(request.LeagueId);

            if (league == null)
                return null;


            Random rand = new Random();
            var teamToRemove = league.Teams[rand.Next(league.Teams.Count()) - 1];

            var teamToRemovePlayers = await _unitOfWork.PlayerRepository.GetAllPlayersByTeam(teamToRemove.TeamId, 0, null);

            team.Players = teamToRemovePlayers.PageResults;
            team.CurrentTeamSheet.UpdateRating(team.Players);

            await _unitOfWork.TeamRepository.UpdateTeam(team);

            await _unitOfWork.FixtureRepository.ClearLeagueFixtures(league.LeagueId);

            league.RemoveTeam(teamToRemove);
            league.Teams.Add(team);
            team.CurrentLeague = league;

            await _unitOfWork.TeamRepository.AddTeam(team);
            await Task.Run(() => league.CreateFixtures());
            await _unitOfWork.Save();

            return team;
        }
    }
}
