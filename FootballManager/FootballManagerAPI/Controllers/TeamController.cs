using Application.Commands;
using Application.Pagination;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Application.Dto;
using Application.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> GetAllTeams(int pg = 1)
        {
            _logger.LogInformation("Preparing to get all teams...");

            var result = await _mediator.Send(new GetAllTeams() 
            { 
                Page = pg,
            });

            if (result == null)
            {
                _logger.LogError("Couldn't get all teams!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<TeamGetDto>>(result);

            _logger.LogInformation("All teams received successfully!!!");

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("List")]
        [Authorize]
        public async Task<IActionResult> GetAllTeamsList()
        {
            _logger.LogInformation("Preparing to get all teams list...");

            var result = await _mediator.Send(new GetAllTeamsList());

            if (result == null)
            {
                _logger.LogError("Couldn't get all teams list!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<TeamGetDto>>(result);

            _logger.LogInformation("All teams list received successfully!!!");

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> GetTeamPlayersById(int id, [FromQuery] PlayerFilter filter = null, int pg = 1)
        {
            _logger.LogInformation($"Preparing to get players of team with id {id}...");

            var query = new GetPlayersByTeam { 
                TeamId = id,
                Page = pg,
                Filter = filter
            };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"Team with id {id} not found!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<ShortPlayerGetDto>>(result.PageResults);

            var page = new Pager<ShortPlayerGetDto>(result.TotalResults, result.CurrentPage, result.PageNumOfResults) { PageResults = mappedResult };

            _logger.LogInformation($"Players of team with id {id} received successfully!!!");

            return Ok(page);
        }

        [HttpGet]
        [Route("{id}/Fixtures/{pg?}")]
        [Authorize]
        public async Task<IActionResult> GetTeamFixturesById(int id)
        {
            _logger.LogInformation($"Preparing to get fixtures of team with id {id}...");

            var query = new GetFixturesByTeam { 
                TeamId = id,
            };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"Team with id {id} not found!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<FixtureGetDto>>(result);

            _logger.LogInformation($"Fixtures of team with id {id} received successfully!!!");

            return Ok(mappedResult);
        }


        [HttpPut]
        [Route("{teamId}")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
    }
}
