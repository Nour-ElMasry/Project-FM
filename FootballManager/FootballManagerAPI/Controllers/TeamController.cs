using Application.Commands;
using Application.Queries;
using AutoMapper;
using FootballManagerAPI.Dto;
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

        public TeamController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var result = await _mediator.Send(new GetAllTeams());
            var mappedResult = _mapper.Map<List<TeamGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            var query = new GetTeamById { TeamId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<TeamGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/Players")]
        public async Task<IActionResult> GetTeamPlayersById(int id)
        {
            var query = new GetPlayersByTeam { TeamId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<List<PlayerGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/Fixtures")]
        public async Task<IActionResult> GetTeamFixturesById(int id)
        {
            var query = new GetFixturesByTeam { TeamId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<List<FixtureGetDto>>(result);
            return Ok(mappedResult);
        }


        [HttpPut]
        [Route("{teamId}")]
        public async Task<IActionResult> UpdateTeam(int teamId, [FromBody] TeamPutPostDto updated)
        {
            var command = new UpdateTeam
            {
                TeamId = teamId,
                Name = updated.Name,
                Country = updated.Country,
                Venue = updated.Venue
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }

        [Route("{teamId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTeam(int teamId)
        {
            var command = new DeleteTeam { TeamId = teamId };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }

        [Route("{teamId}/Players/AddPlayer/{playerId}")]
        [HttpPut]
        public async Task<IActionResult> AddPlayerFromTeam(int teamId, int playerId)
        {
            var command = new AddPlayerToTeam { TeamId = teamId, PlayerId = playerId };
            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();
            
           return NoContent();
        }

        [Route("{teamId}/Players/{playerId}")]
        [HttpDelete]
        public async Task<IActionResult> RemovePlayerFromTeam(int teamId, int playerId)
        {
            var command = new RemovePlayerFromTeam { TeamId = teamId, PlayerId = playerId };
            var result = await _mediator.Send(command);

            return NoContent();
        }
    }
}
