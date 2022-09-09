using Application.Commands;
using Application.Queries;
using AutoMapper;
using FootballManagerAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballManagerAPI.Controllers
{
    [Route("api/v1/Players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public PlayerController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] PlayerPutPostDto player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = _mapper.Map<CreatePlayer>(player);

            var created = await _mediator.Send(command);
            var dto = _mapper.Map<PlayerGetDto>(created);

            return CreatedAtAction(nameof(GetPlayerById), new { id = created.PlayerId }, dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            var result = await _mediator.Send(new GetAllPlayers());
            var mappedResult = _mapper.Map<List<PlayerGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            var query = new GetPlayerById { PlayerId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<PlayerGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var command = new DeletePlayer { PlayerId = id };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        [Route("{playerId}")]
        public async Task<IActionResult> UpdatePlayer(int playerId, [FromBody] PlayerPutPostDto updated)
        {
            var command = new UpdatePlayer
            {
                PlayerId = playerId,
                Name = updated.Name,
                Country = updated.Country,
                DateOfBirth = updated.DateOfBirth,
                Position = updated.Position,
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }
    }
}
