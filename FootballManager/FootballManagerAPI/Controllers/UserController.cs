using Application.Commands;
using Application.Pagination;
using Application.Queries;
using AutoMapper;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;


namespace Application.Controllers
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
        [Route("All/{pg?}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers(int pg = 1)
        {
            _logger.LogInformation("Preparing to get all users...");

            var result = await _mediator.Send(new GetAllUsers { Page = pg });

            if (result == null)
            {
                _logger.LogError("Couldn't get all users!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<UserGetDto>>(result.PageResults);

            var page = new Pager<UserGetDto>(result.TotalResults, result.CurrentPage) { PageResults = mappedResult };

            _logger.LogInformation("All users received successfully!!!");

            return Ok(page);
        }

        [HttpGet]
        [Route("unique/{username}")]
        public async Task<IActionResult> CheckUnique(string username)
        {
            _logger.LogInformation("Preparing to check if username is unique...");

            var result = await _mediator.Send(new CheckUniqueUsernameCommand
            { 
                Username = username
            });

            if (result)
            {
                _logger.LogInformation("Username is unique!!!");
            }
            else {
                _logger.LogInformation("Username is not unique!!!");
            }

            return Ok(result);
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

            _logger.LogInformation("User created successfully!!!");

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] UserPostDto user)
        {
            _logger.LogInformation("Preparing to create an admin user...");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Information received was invalid!!");
                return BadRequest(ModelState);
            }

            var command = _mapper.Map<CreateAdmin>(user);

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"Failed to create an admin user!!!");
                return NotFound();
            }

            _logger.LogInformation("An Admin User created successfully!!!");

            return Ok(result);
        }


        [HttpPost]
        [Route("{id}/CreateTeam")]
        [Authorize]
        public async Task<IActionResult> CreateUserTeam([FromBody] TeamPutPostDto team, string id)
        {
            _logger.LogInformation("Preparing to create a User Team...");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Information received was invalid!!");
                return BadRequest(ModelState);
            }

            var userToManagerCommand = new CreateRealManager
            {
                UserManagerId = id
            };

            var userToManager = await _mediator.Send(userToManagerCommand);

            if (userToManager == null)
            {
                _logger.LogError($"User with id {id} not found!!");
                return NotFound();
            }

            if(userToManager.CurrentTeam != null)
            {
                _logger.LogError($"User with id {id} already assigned to a team!!");
                return Conflict();
            }

            var command = _mapper.Map<CreateTeam>(team);    

            var createdTeam = await _mediator.Send(command);

            if(createdTeam == null)
            {
                _logger.LogError($"League with id {id} not found!!");
                return NotFound();
            }

            var userAssignedToTeam = await _mediator.Send(new AddManagerToTeam 
            {
                ManagerId = userToManager.ManagerId,
                TeamId = createdTeam.TeamId
            });

            var leagueReset = await _mediator.Send(new ResetLeagues());

            if (leagueReset == null)
            {
                _logger.LogError($"No Leagues found to reset!!");
                return NotFound();
            }

            var dto = _mapper.Map<TeamGetDto>(userAssignedToTeam);

            _logger.LogInformation("User Team created successfully!!!");

            return CreatedAtAction(nameof(GetUserTeam), new { id = userAssignedToTeam.TeamId }, dto);
        }


        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserPutDto updated)
        {
            _logger.LogInformation($"Preparing to update user with id {id}...");

            var command = new UpdateUser
            {
                UserId = id,
                Name = updated.Name,
                Country = updated.Country,
                DateOfBirth = updated.DateOfBirth,
                Image = updated.Image,
            };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"User with id {id} not found!!!");
                return NotFound();
            }

            _logger.LogInformation($"User with id {id} updated successfully!!!");

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(string id)
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
        [Authorize]
        public async Task<IActionResult> GetUserTeam(string id)
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
        public async Task<IActionResult> LoginUser(UserAuthDto user)
        {
            _logger.LogInformation($"Preparing to authenticate user...");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Information received was invalid!!");
                return BadRequest(ModelState);
            }

            var command = new LoginUser
            {
                UserName = user.Username,
                Password = user.Password
            };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError("Failed to authenticate user!!!");
                return Unauthorized();
            }

            _logger.LogInformation("User authenticated successfully!!!");

            return Ok(result);
        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogInformation($"Preparing to delete user with id {id}...");

            var command = new GetTeamByUserId
            {
                UserId = id
            };

            var result = await _mediator.Send(command);

            if (result != null)
            {
                var command1 = new DeleteTeam { TeamId = result.TeamId };
                var result1 = await _mediator.Send(command1);

                if (result1 == null)
                {
                    _logger.LogError($"Team with id {result.TeamId} not found!!");
                    return NotFound();
                }
            }

            var command2 = new DeleteUser { UserId = id };
            var result2 = await _mediator.Send(command2);

            if (result2 == null)
            {
                _logger.LogError($"User with id {id} not found!!!");
                return NotFound();
            }

            _logger.LogInformation($"User with id {id} deleted successfully!!!");

            return NoContent();
        }
    }
}
