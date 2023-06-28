using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Application.Dto;
using FootballManagerAPI.Dto;

namespace Application.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamGetDto>()
                .ForMember(td => td.Id, opt => opt.MapFrom(t => t.TeamId))
                .ForMember(td => td.Name, opt => opt.MapFrom(t => t.Name))
                .ForMember(td => td.Country, opt => opt.MapFrom(t => t.Country))
                .ForMember(td => td.Venue, opt => opt.MapFrom(t => t.Venue))
                .ForMember(td => td.Logo, opt => opt.MapFrom(t => t.Logo))
                .ForMember(td => td.TeamManager, opt => opt.MapFrom(t => t.TeamManager.ManagerPerson))
                .ForMember(td => td.CurrentTeamSheet, opt => opt.MapFrom(t => new TeamSheetDto
                {
                    AttackingRating = t.CurrentTeamSheet.AttackingRating,
                    DefendingRating = t.CurrentTeamSheet.DefendingRating,
                }))
                .ForMember(td => td.CurrentLeague, opt => opt.MapFrom(t => new ShortLeagueGetDto { 
                    LeagueId = t.CurrentLeague.LeagueId,
                    LeagueName = t.CurrentLeague.Name, 
                    LeagueLogo = t.CurrentLeague.LeagueLogo
                }));

            CreateMap<Team, LineUpDto>()
               .ForMember(td => td.TeamId, opt => opt.MapFrom(t => t.TeamId))
               .ForMember(td => td.Formation, opt => opt.MapFrom(t => new FormationDto
               {
                   Defenders = t.CurrentTeamSheet.TeamFormation.Defenders,
                   Midfielders = t.CurrentTeamSheet.TeamFormation.Midfielders,
                   Attackers = t.CurrentTeamSheet.TeamFormation.Attackers
               }))
               .ForMember(td => td.StartingEleven, opt => opt.MapFrom(t => t.CurrentTeamSheet.StartingEleven.Select(p => new LineUpPlayersDto
               {
                   PlayerPerson = p.PlayerPerson,
                   PlayerStats = p.CurrentPlayerStats,
                   Position = p.Position,
                   Id = p.PlayerId,
               })))
               .ForMember(td => td.Bench, opt => opt.MapFrom(t => t.CurrentTeamSheet.Bench.Select(p => new LineUpPlayersDto
               {
                   PlayerPerson = p.PlayerPerson,
                   PlayerStats = p.CurrentPlayerStats,
                   Position = p.Position,
                   Id = p.PlayerId,
               })));
;

            CreateMap<Team, TeamGetAllDto>()
                .ForMember(td => td.Id, opt => opt.MapFrom(t => t.TeamId))
                .ForMember(td => td.Name, opt => opt.MapFrom(t => t.Name))
                .ForMember(td => td.Country, opt => opt.MapFrom(t => t.Country))
                .ForMember(td => td.Venue, opt => opt.MapFrom(t => t.Venue))
                .ForMember(td => td.Logo, opt => opt.MapFrom(t => t.Logo))
                .ForMember(td => td.Tactic, opt => opt.MapFrom(t => GetTeamTactic(t.CurrentTeamSheet)))
                .ForMember(td => td.TeamManager, opt => opt.MapFrom(t => t.TeamManager.ManagerPerson))
                .ForMember(td => td.CurrentTeamSheet, opt => opt.MapFrom(t => t.CurrentTeamSheet))
                .ForMember(td => td.CurrentLeague, opt => opt.MapFrom(t => new ShortLeagueGetDto
                {
                    LeagueId = t.CurrentLeague.LeagueId,
                    LeagueName = t.CurrentLeague.Name,
                    LeagueLogo = t.CurrentLeague.LeagueLogo
                }));

            CreateMap<TeamPutPostDto, CreateTeam>()
                .ForMember(td => td.Name, opt => opt.MapFrom(t => t.Name))
                .ForMember(td => td.Country, opt => opt.MapFrom(t => t.Country))
                .ForMember(td => td.Venue, opt => opt.MapFrom(t => t.Venue))
                .ForMember(td => td.Logo, opt => opt.MapFrom(t => t.Logo))
                .ForMember(td => td.LeagueId, opt => opt.MapFrom(t => t.LeagueId));

        }

        private string GetTeamTactic(TeamSheet teamSheet)
        {
            if (teamSheet.TeamTactic is AttackingTactic)
            {
                return "attacking";
            }
            else if (teamSheet.TeamTactic is DefendingTactic)
            {
                return "defending";
            }
            return "balanced";
        }
    }
}
