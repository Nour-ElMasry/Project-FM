using Application.Commands;
using Application.Queries;
using AutoMapper;
using FootballManagerAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballManagerAPI.Controllers
{
    [Route("api/v1/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public readonly ILogger _logger;

        public UserController(IMapper mapper, IMediator mediator, ILogger<object> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("User Controller called...");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("Preparing to get all users...");

            var result = await _mediator.Send(new GetAllUsers());

            if (result == null)
            {
                _logger.LogError("Couldn't get all users!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<UserGetDto>>(result);

            _logger.LogInformation("All users received successfully!!!");

            return Ok(mappedResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserPostDto user)
        {
            _logger.LogInformation("Preparing to create a user...");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Information received was invalid!!");
                return BadRequest(ModelState);
            }

            var command = _mapper.Map<CreateUser>(user);

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"Failed to create user!!!");
                return NotFound();
            }

            var dto = _mapper.Map<UserGetDto>(result);

            _logger.LogInformation("User created successfully!!!");

            return CreatedAtAction(nameof(GetUserById), new { id = result.UserId }, dto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            _logger.LogInformation($"Preparing to get user with id {id}...");

            var command = new GetUserById
            {
                UserId = id
            };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"User with id {id} not found!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<UserGetDto>(result);


            _logger.LogInformation($"User with id {id} received successfully!!!");

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}/Team")]
        public async Task<IActionResult> GetUserTeam(int id)
        {
            _logger.LogInformation($"Preparing to get team of user with id {id}...");

            var command = new GetTeamByUserId
            {
                UserId = id
            };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"User with id {id} not found or is not a manager of a team!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<TeamGetDto>(result);

            _logger.LogInformation($"Team of user with id {id} received successfully!!!");

            return Ok(mappedResult);
        }

        [HttpPost]
        [Route("Auth")]
        public async Task<IActionResult> AuthUser(UserAuthDto user)
        {
            _logger.LogInformation($"Preparing to authenticate user...");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Information received was invalid!!");
                return BadRequest(ModelState);
            }

            var command = new AuthUser
            {
                UserName = user.Username,
                Password = user.Password
            };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError("Failed to authenticate user!!!");
                return NotFound();
            }

            _logger.LogInformation("User authenticated successfully!!!");

            return NoContent();
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation($"Preparing to delete user with id {id}...");

            var command = new DeleteUser { UserId = id };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"User with id {id} not found!!!");
                return NotFound();
            }

            _logger.LogInformation($"User with id {id} deleted successfully!!!");

            return NoContent();
        }
    }
}
