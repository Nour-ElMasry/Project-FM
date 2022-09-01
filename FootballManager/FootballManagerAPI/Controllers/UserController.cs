using Application.Commands;
using Application.Queries;
using AutoMapper;
using FootballManagerAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballManagerAPI.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public UserController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        [Route("{id}/Team")]
        public async Task<IActionResult> GetUserTeam(int id)
        {
            var command = new GetTeamByUserId
            {
                UserId = id
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<TeamGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        [Route("Auth")]
        public async Task<IActionResult> AuthUser(UserAuthDto user)
        {
            var command = new AuthUser
            {
                UserName = user.Username,
                Password = user.Password
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }
    }
}
