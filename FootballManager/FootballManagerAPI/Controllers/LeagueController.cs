using Application.Commands;
using Application.Queries;
using AutoMapper;
using FootballManagerAPI.Dto;
using FootballManagerAPI.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballManagerAPI.Controllers
{
    [Route("api/v1/Leagues")]
    [ApiController]
    public class LeagueController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public readonly ILogger _logger;

        public LeagueController(IMapper mapper, IMediator mediator, ILogger<object> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("League Controller is called...");
        }

        [HttpGet]
        [Route("All/{pg?}")]
        public async Task<IActionResult> GetAllLeagues(int pg = 1)
        {
            _logger.LogInformation("Preparing to get all leagues...");

            var result = await _mediator.Send(new GetAllLeagues());

            if (result == null)
            {
                _logger.LogError("Couldn't get all Leagues!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<LeagueGetDto>>(result);

            var page = new Pager<LeagueGetDto>(mappedResult.Count, pg);

            var pageResults = mappedResult
                .Skip((page.CurrentPage - 1) * page.PageNumOfResults)
                .Take(page.PageNumOfResults)
                .ToList();

            page.PageResults = pageResults;

            _logger.LogInformation("All leagues have been received successfully!!!");

            return Ok(page);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetLeagueById(int id)
        {
            _logger.LogInformation($"Preparing to get league with id {id}...");

            var query = new GetLeagueById { LeagueId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"League with id {id} was not found!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<LeagueGetDto>(result);

            _logger.LogInformation($"League with id {id} has been received successfully!!!");

            return Ok(mappedResult);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteLeague(int id)
        {
            _logger.LogInformation($"Preparing to delete league with id {id}...");

            var command = new DeleteLeague { LeagueId = id };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"League with id {id} was not found!!!");
                return NotFound();
            }

            _logger.LogInformation($"League with id {id} has been deleted successfully!!!");

            return NoContent();
        }

        [HttpGet]
        [Route("{id}/Teams/{pg?}")]
        public async Task<IActionResult> GetLeagueTeamsById(int id, int pg = 1)
        {
            _logger.LogInformation($"Preparing to get teams of league with id {id}...");

            var query = new GetTeamsByLeague { LeagueId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"League with id {id} was not found!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<TeamGetDto>>(result);

            var page = new Pager<TeamGetDto>(mappedResult.Count, pg);

            var pageResults = mappedResult
                .Skip((page.CurrentPage - 1) * page.PageNumOfResults)
                .Take(page.PageNumOfResults)
                .ToList();

            page.PageResults = pageResults;

            _logger.LogInformation($"Teams of league with id {id} have been received successfully!!!");

            return Ok(page);
        }


        [HttpDelete]
        [Route("{id}/Teams/{teamId}")]
        public async Task<IActionResult> RemoveTeamFromLeague(int id, int teamId)
        {
            _logger.LogInformation($"Preparing to remove team with id {teamId} from league with id {id}...");

            var command = new RemoveTeamFromLeague { LeagueId = id, TeamId = teamId };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"League or Team not found!!!");
                return NotFound();
            }

            _logger.LogInformation($"Succefully removed team with id {teamId} from league with id {id}!!!");

            return NoContent();
        }

        [HttpGet]
        [Route("{id}/Players/{pg?}")]
        public async Task<IActionResult> GetLeaguePlayersById(int id, int pg = 1)
        {
            _logger.LogInformation($"Preparing to get players from league with id {id}...");

            var query = new GetPlayersByLeague { LeagueId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"League with id {id} was not found!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<PlayerGetDto>>(result);

            var page = new Pager<PlayerGetDto>(mappedResult.Count, pg);

            var pageResults = mappedResult
                .Skip((page.CurrentPage - 1) * page.PageNumOfResults)
                .Take(page.PageNumOfResults)
                .ToList();

            page.PageResults = pageResults;

            _logger.LogInformation($"Players from league with id {id} have been received successfully!!!");

            return Ok(page);
        }

        [HttpGet]
        [Route("{id}/Fixtures/{pg?}")]
        public async Task<IActionResult> GetLeagueFixturesById(int id, int pg = 1)
        {
            _logger.LogInformation($"Preparing to get fixtures from league with id {id}...");

            var query = new GetFixturesByLeague { LeagueId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"League with id {id} was not found!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<FixtureGetDto>>(result);

            var page = new Pager<FixtureGetDto>(mappedResult.Count, pg);

            var pageResults = mappedResult
                .Skip((page.CurrentPage - 1) * page.PageNumOfResults)
                .Take(page.PageNumOfResults)
                .ToList();

            page.PageResults = pageResults;

            _logger.LogInformation($"Fixtures from league with id {id} have been received successfully!!!");

            return Ok(page);
        }

        [HttpPut]
        [Route("{id}/Fixtures/Simulate")]
        public async Task<IActionResult> SimulateAllLeagueFixture(int id)
        {
            _logger.LogInformation($"Preparing to simulate fixtures from league with id {id}...");

            var query = new SimulateAllFixtures { LeagueId = id };

            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"League with id {id} was not found!!!");
                return NotFound();
            }

            _logger.LogInformation($"Fixtures from league with id {id} simulated successfully!!!");

            return NoContent();
        }


        [HttpGet]
        [Route("{id}/GenerateFixtures/{pg?}")]
        public async Task<IActionResult> GenerateLeagueFixture(int id, int pg)
        {
            _logger.LogInformation($"Preparing to generate fixtures for league with id {id}...");

            var query = new GenerateLeagueFixtures { LeagueId = id };

            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"League with id {id} was not found!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<FixtureGetDto>>(result);

            var page = new Pager<FixtureGetDto>(mappedResult.Count, pg);

            var pageResults = mappedResult
                .Skip((page.CurrentPage - 1) * page.PageNumOfResults)
                .Take(page.PageNumOfResults)
                .ToList();

            page.PageResults = pageResults;

            _logger.LogInformation($"Fixtures for league with id {id} generated successfully!!!");

            return Ok(page);
        }

        [HttpGet]
        [Route("{id}/Fixtures/{fixtureId}/SimulateFixture")]
        public async Task<IActionResult> SimulateALeagueFixture(int id, int fixtureId)
        {
            _logger.LogInformation($"Preparing to simulate a fixture with id {fixtureId} from league with id {id}...");

            var query = new SimulateAFixture { LeagueId = id, FixtureID = fixtureId };

            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"League/Fixture not found!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<FixtureGetDto>(result);

            _logger.LogInformation($"Fixture with id {fixtureId} from league with id {id} simulated successfully!!!");

            return Ok(mappedResult);
        }


        [HttpGet]
        [Route("{id}/NextSeason")]
        public async Task<IActionResult> NextLeagueSeason(int id)
        {
            _logger.LogInformation($"Preparing to update league with id {id} to next season...");

            var query = new NextSeason { LeagueId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"League with id {id} was not found!!!");
                return NotFound();
            }

            _logger.LogInformation($"League with id {id} updated successfully!!!");

            return NoContent();
        }
    }
}

