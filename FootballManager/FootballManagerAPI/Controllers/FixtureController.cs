using Application.Commands;
using Application.Queries;
using AutoMapper;
using FootballManagerAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballManagerAPI.Controllers
{
    [Route("api/v1/Fixtures")]
    [ApiController]
    public class FixtureController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public FixtureController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFixtures()
        {
            var result = await _mediator.Send(new GetAllFixtures());
            var mappedResult = _mapper.Map<List<FixtureGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFixtureById(int id)
        {
            var query = new GetFixtureById { FixtureId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<FixtureGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpPut]
        [Route("{fixutreId}")]
        public async Task<IActionResult> UpdateFixture(int fixutreId, [FromBody] FixturePutDto updated)
        {
            var command = new UpdateFixture
            {
                FixtureId = fixutreId,
                newDate = updated.Date
            };

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }
    }
}
