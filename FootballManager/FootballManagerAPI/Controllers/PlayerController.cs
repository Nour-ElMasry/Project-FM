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
        public readonly ILogger _logger;

        public PlayerController(IMapper mapper, IMediator mediator, ILogger<object> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("Player controller is called...");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] PlayerPutPostDto player)
        {
            _logger.LogInformation("Preparing to create a player...");

            if (!ModelState.IsValid) {
                _logger.LogError("Information received was invalid!!");
                return BadRequest(ModelState);
            } 

            var command = _mapper.Map<CreatePlayer>(player);

            var created = await _mediator.Send(command);
            var dto = _mapper.Map<PlayerGetDto>(created);

            _logger.LogInformation("Player created successfully!!!");

            return CreatedAtAction(nameof(GetPlayerById), new { id = created.PlayerId }, dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            _logger.LogInformation("Preparing to get all players...");

            var result = await _mediator.Send(new GetAllPlayers());

            if (result == null) {
                _logger.LogError("Couldn't get all players!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<PlayerGetDto>>(result);

            _logger.LogInformation("All players received successfully!!!");

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            _logger.LogInformation($"Preparing to get a player with id {id}...");

            var query = new GetPlayerById { PlayerId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"Player with id {id} not found!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<PlayerGetDto>(result);

            _logger.LogInformation($"Player with id {id} received successfully!!!");

            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            _logger.LogInformation($"Preparing to delete player with id {id}...");

            var command = new DeletePlayer { PlayerId = id };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"Player with id {id} not found!!!");
                return NotFound();
            }

            _logger.LogInformation($"Player with id {id} deleted successfully!!!");

            return NoContent();
        }

        [HttpPut]
        [Route("{playerId}")]
        public async Task<IActionResult> UpdatePlayer(int playerId, [FromBody] PlayerPutPostDto updated)
        {
            _logger.LogInformation($"Preparing to update player with id {playerId}...");

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
            {
                _logger.LogError($"Player with id {playerId} not found!!!");
                return NotFound();
            }

            _logger.LogInformation($"Player with id {playerId} updated successfully!!!");

            return NoContent();
        }
    }
}
