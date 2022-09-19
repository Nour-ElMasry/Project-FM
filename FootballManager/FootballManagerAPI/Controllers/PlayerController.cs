using Application.Commands;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using FootballManagerAPI.Dto;
using FootballManagerAPI.Filters;
using FootballManagerAPI.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            if (!ModelState.IsValid)
            {
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
        [Route("All/{pg?}")]
        public async Task<IActionResult> GetAllPlayers([FromQuery] PlayerFilter filter = null, int pg = 1)
        {
            _logger.LogInformation("Preparing to get all players...");

            var result = await _mediator.Send(new GetAllPlayers());

            if (result == null)
            {
                _logger.LogError("Couldn't get all players!!!");
                return NotFound();
            }


            if (!filter.IsValidYearRange())
            {
                _logger.LogError("Date range invalid!!!");
                return BadRequest("Date range invalid!!!");
            }

            result = await ApplyPlayerFilter(filter, result);

            var mappedResult = _mapper.Map<List<PlayerGetDto>>(result);

            if (pg == 0)
            {
                return Ok(mappedResult);
            }

            var page = new Pager<PlayerGetDto>(mappedResult.Count, pg);

            var pageResults = mappedResult
                .Skip((page.CurrentPage - 1) * page.PageNumOfResults)
                .Take(page.PageNumOfResults)
                .ToList();

            page.PageResults = pageResults;

            _logger.LogInformation("All players received successfully!!!");

            return Ok(page);
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

        private async Task<List<Player>> ApplyPlayerFilter(PlayerFilter filter, List<Player> result)
        {

            if (filter.TeamId == 0 &&
                String.IsNullOrWhiteSpace(filter.Name) &&
                String.IsNullOrWhiteSpace(filter.Country) &&
                String.IsNullOrWhiteSpace(filter.Position) &&
                filter.MinYearOfBirth == 0 &&
                filter.MaxYearOfBirth == 0)
                return result;

            if (filter.TeamId != 0)
                result = await Task.Run(() => result.Where(p =>
                    p.CurrentTeam.TeamId == filter.TeamId
                ).ToList());

            if (!String.IsNullOrWhiteSpace(filter.Name))
                result = await Task.Run(() => result.Where(p =>
                    p.PlayerPerson.Name.Contains(filter.Name, StringComparison.CurrentCultureIgnoreCase)
                ).ToList());

            if (!String.IsNullOrWhiteSpace(filter.Country))
                result = await Task.Run(() => result.Where(p =>
                    p.PlayerPerson.Country == filter.Country
                ).ToList());

            if (!String.IsNullOrWhiteSpace(filter.Position))
                result = await Task.Run(() => result.Where(p =>
                    p.Position == filter.Position
                ).ToList());

            if (filter.MinYearOfBirth != 0 && filter.MaxYearOfBirth != 0)
                result = await Task.Run(() => result.Where(p =>
                    p.PlayerPerson.BirthDate.Value.Year >= filter.MinYearOfBirth &&
                    p.PlayerPerson.BirthDate.Value.Year <= filter.MaxYearOfBirth
                ).ToList());

            if (filter.MinYearOfBirth != 0)
            {
                result = await Task.Run(() => result.Where(p =>
                    p.PlayerPerson.BirthDate.Value.Year >= filter.MinYearOfBirth
                ).ToList());
            }

            if (filter.MaxYearOfBirth != 0)
            {
                result = await Task.Run(() => result.Where(p =>
                       p.PlayerPerson.BirthDate.Value.Year <= filter.MaxYearOfBirth
                   ).ToList());
            }

            return result;
        }
    }
}
