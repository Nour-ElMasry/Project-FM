using Application.Commands;
using Application.Queries;
using AutoMapper;
using FootballManagerAPI.Dto;
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

        public LeagueController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeagues()
        {
            var result = await _mediator.Send(new GetAllLeagues());
            var mappedResult = _mapper.Map<List<LeagueGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetLeagueById(int id)
        {
            var query = new GetLeagueById { LeagueId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<LeagueGetDto>(result);
            return Ok(mappedResult);
        }


        [HttpGet]
        [Route("{id}/Teams")]
        public async Task<IActionResult> GetLeagueTeamsById(int id)
        {
            var query = new GetTeamsByLeague { LeagueId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<List<TeamGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/Players")]
        public async Task<IActionResult> GetLeaguePlayersById(int id)
        {
            var query = new GetPlayersByLeague { LeagueId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<List<PlayerGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/Fixtures")]
        public async Task<IActionResult> GetLeagueFixturesById(int id)
        {
            var query = new GetFixturesByLeague { LeagueId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<List<FixtureGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpPut]
        [Route("{id}/Fixtures/Simulate")]
        public async Task<IActionResult> SimulateAllLeagueFixture(int id)
        {
            var query = new SimulateAllFixtures { LeagueId = id };
            
            await _mediator.Send(query);

            return NoContent();
        }


        [HttpPut]
        [Route("{id}/GenerateFixtures")]
        public async Task<IActionResult>  GenerateLeagueFixture(int id)
        {
            var query = new GenerateLeagueFixtures { LeagueId = id };

            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<LeagueGetDto>(result);

            return Ok(mappedResult);
        }

        [HttpPut]
        [Route("{id}/Fixtures/{fixtureId}/SimulateFixture")]
        public async Task<IActionResult> SimulateALeagueFixture(int id, int fixtureId)
        {
            var query = new SimulateAFixture { LeagueId = id , FixtureID = fixtureId };

            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<FixtureGetDto>(result);

            return Ok(mappedResult);
        }


        [HttpPut]
        [Route("{id}/NextSeason")]
        public async Task<IActionResult> NextLeagueSeason(int id)
        {
            var query = new NextSeason { LeagueId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return NoContent();
        }
    }
}

