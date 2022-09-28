﻿using Application.Commands;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using FootballManagerAPI.Dto;
using FootballManagerAPI.Pagination;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;

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
        [Route("All/{pg?}")]
        public async Task<IActionResult> GetAllUsers(int pg = 1)
        {
            _logger.LogInformation("Preparing to get all users...");

            var result = await _mediator.Send(new GetAllUsers());

            if (result == null)
            {
                _logger.LogError("Couldn't get all users!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<UserGetDto>>(result);

            var page = new Pager<UserGetDto>(mappedResult.Count, pg);

            var pageResults = mappedResult
                .Skip((page.CurrentPage - 1) * page.PageNumOfResults)
                .Take(page.PageNumOfResults)
                .ToList();

            page.PageResults = pageResults;

            _logger.LogInformation("All users received successfully!!!");

            return Ok(page);
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

            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, dto);
        }

        [HttpPost]
        [Route("{id}/CreateTeam")]
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

            var userAssignedToTeam = await _mediator.Send(new AddManagerToTeam 
            {
                ManagerId = userToManager.ManagerId,
                TeamId = createdTeam.TeamId
            });

            var dto = _mapper.Map<TeamGetDto>(userAssignedToTeam);

            _logger.LogInformation("User Team created successfully!!!");

            return CreatedAtAction(nameof(GetUserTeam), new { id = userAssignedToTeam.TeamId }, dto);
        }


        [HttpPut]
        [Route("{id}")]
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
        public async Task<IActionResult> DeleteUser(string id)
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
