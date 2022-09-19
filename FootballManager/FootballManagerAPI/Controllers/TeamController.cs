using Application.Commands;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using FootballManagerAPI.Dto;
using FootballManagerAPI.Filters;
using FootballManagerAPI.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballManagerAPI.Controllers
{
    [Route("api/v1/Teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public readonly ILogger _logger;

        public TeamController(IMapper mapper, IMediator mediator, ILogger<object> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;

            _logger.LogInformation("Team controller called...");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] TeamPutPostDto team)
        {
            _logger.LogInformation("Preparing to create a Team...");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Information received was invalid!!");
                return BadRequest(ModelState);
            }

            var command = _mapper.Map<CreateTeam>(team);

            var created = await _mediator.Send(command);
            var dto = _mapper.Map<TeamGetDto>(created);

            _logger.LogInformation("Team created successfully!!!");

            return CreatedAtAction(nameof(GetTeamById), new { id = created.TeamId }, dto);
        }


        [HttpGet]
        [Route("All/{pg?}")]
        public async Task<IActionResult> GetAllTeams([FromQuery] TeamFilter filter = null, int pg = 1)
        {
            _logger.LogInformation("Preparing to get all teams...");

            var result = await _mediator.Send(new GetAllTeams());

            if (result == null)
            {
                _logger.LogError("Couldn't get all teams!!");
                return NotFound();
            }

            result = await ApplyTeamFilter(filter, result);

            var mappedResult = _mapper.Map<List<TeamGetDto>>(result);

            if(pg == 0)
            {
                return Ok(mappedResult);
            }

            var page = new Pager<TeamGetDto>(mappedResult.Count, pg);

            var pageResults = mappedResult
                .Skip((page.CurrentPage - 1) * page.PageNumOfResults)
                .Take(page.PageNumOfResults)
                .ToList();

            page.PageResults = pageResults;

            _logger.LogInformation("All teams received successfully!!!");

            return Ok(page);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            _logger.LogInformation($"Preparing to get team with id {id}...");

            var query = new GetTeamById { TeamId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"Team with id {id} not found!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<TeamGetDto>(result);

            _logger.LogInformation($"Team with id {id} received successfully!!!");

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/Players/{pg?}")]
        public async Task<IActionResult> GetTeamPlayersById(int id, int pg = 1)
        {
            _logger.LogInformation($"Preparing to get players of team with id {id}...");

            var query = new GetPlayersByTeam { TeamId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"Team with id {id} not found!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<ShortPlayerGetDto>>(result);

            var page = new Pager<ShortPlayerGetDto>(mappedResult.Count, pg);

            var pageResults = mappedResult
                .Skip((page.CurrentPage - 1) * page.PageNumOfResults)
                .Take(page.PageNumOfResults)
                .ToList();

            page.PageResults = pageResults;

            _logger.LogInformation($"Players of team with id {id} received successfully!!!");

            return Ok(page);
        }

        [HttpGet]
        [Route("{id}/Fixtures/{pg?}")]
        public async Task<IActionResult> GetTeamFixturesById(int id, int pg = 1)
        {
            _logger.LogInformation($"Preparing to get fixtures of team with id {id}...");

            var query = new GetFixturesByTeam { TeamId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"Team with id {id} not found!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<FixtureGetDto>>(result);

            var page = new Pager<FixtureGetDto>(mappedResult.Count, pg);

            var pageResults = mappedResult
                .Skip((page.CurrentPage - 1) * page.PageNumOfResults)
                .Take(page.PageNumOfResults)
                .ToList();

            page.PageResults = pageResults;


            _logger.LogInformation($"Fixtures of team with id {id} received successfully!!!");

            return Ok(page);
        }


        [HttpPut]
        [Route("{teamId}")]
        public async Task<IActionResult> UpdateTeam(int teamId, [FromBody] TeamPutPostDto updated)
        {
            _logger.LogInformation($"Preparing to update team with id {teamId}...");

            var command = new UpdateTeam
            {
                TeamId = teamId,
                Name = updated.Name,
                Country = updated.Country,
                Venue = updated.Venue
            };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"Team with id {teamId} not found!!");
                return NotFound();
            }

            _logger.LogInformation($"Team with id {teamId} updated successfully!!!");

            return NoContent();
        }


        [HttpDelete]
        [Route("{teamId}")]
        public async Task<IActionResult> DeleteTeam(int teamId)
        {
            _logger.LogInformation($"Preparing to delete team with id {teamId}...");

            var command = new DeleteTeam { TeamId = teamId };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"Team with id {teamId} not found!!");
                return NotFound();
            }

            _logger.LogInformation($"Team with id {teamId} deleted successfully!!!");

            return NoContent();
        }


        [HttpGet]
        [Route("{teamId}/Players/AddPlayer/{playerId}")]
        public async Task<IActionResult> AddPlayerToTeam(int teamId, int playerId)
        {
            _logger.LogInformation($"Preparing to add player with id {playerId} to team with id {teamId}...");

            var command = new AddPlayerToTeam { TeamId = teamId, PlayerId = playerId };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"Team/Player not found!!");
                return NotFound();
            }

            _logger.LogInformation($"Player with id {playerId} added to team with id {teamId} successfully!!!");

            return NoContent();
        }


        [HttpDelete]
        [Route("{teamId}/Players/{playerId}")]
        public async Task<IActionResult> RemovePlayerFromTeam(int teamId, int playerId)
        {
            _logger.LogInformation($"Preparing to remove player with id {playerId} from team with id {teamId}...");

            var command = new RemovePlayerFromTeam { TeamId = teamId, PlayerId = playerId };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"Team/Player not found!!");
                return NotFound();
            }

            _logger.LogInformation($"Player with id {playerId} removed from team with id {teamId} successfully!!!");

            return NoContent();
        }

        [HttpGet]
        [Route("{id}/AddManager/{managerId}")]
        public async Task<IActionResult> AddManagerToTeam(int id, int managerId)
        {
            _logger.LogInformation($"Preparing to add manager with id {managerId} to team with id {id}...");

            var query = new AddManagerToTeam { TeamId = id, ManagerId = managerId };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"Team/Manager not found!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<TeamGetDto>(result);

            _logger.LogInformation($"Manager with id {managerId} added to team with id {id} successfully!!!");

            return Ok(mappedResult);
        }
        private async Task<List<Team>> ApplyTeamFilter(TeamFilter filter, List<Team> result)
        {

            if (filter.LeagueId == 0 &&
                String.IsNullOrWhiteSpace(filter.Name) &&
                String.IsNullOrWhiteSpace(filter.Country))
                return result;

            if (filter.LeagueId != 0)
                result = await Task.Run(() => result.Where(t =>
                    t.CurrentLeague.LeagueId == filter.LeagueId
                ).ToList());

            if (!String.IsNullOrWhiteSpace(filter.Name))
                result = await Task.Run(() => result.Where(t =>
                    t.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase)
                ).ToList());

            if (!String.IsNullOrWhiteSpace(filter.Country))
                result = await Task.Run(() => result.Where(t =>
                    t.Country == filter.Country
                ).ToList());

            return result;
        }
    }
}
