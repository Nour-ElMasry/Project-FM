using Application.Commands;
using Application.Pagination;
using Application.Queries;
using AutoMapper;
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
