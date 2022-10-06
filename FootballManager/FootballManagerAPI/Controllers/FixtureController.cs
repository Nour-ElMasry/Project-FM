using Application.Commands;
using Application.Pagination;
using Application.Queries;
using AutoMapper;
using FootballManagerAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballManagerAPI.Controllers
{
    [Route("api/v1/Fixtures")]
    [ApiController]
    public class FixtureController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public readonly ILogger _logger;

        public FixtureController(IMapper mapper, IMediator mediator, ILogger<object> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("Fixture Controller is called...");
        }

        [HttpGet]
        [Route("All/{pg?}")]
        [Authorize]
        public async Task<IActionResult> GetAllFixtures(int pg = 1)
        {
            _logger.LogInformation("Preparing to get all Fixtures...");

            var result = await _mediator.Send(new GetAllFixtures { Page = pg });

            if (result.PageResults == null)
            {
                _logger.LogError($"No Fixtures found!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<FixtureGetDto>>(result.PageResults);

            var page = new Pager<FixtureGetDto>(result.TotalResults, result.CurrentPage, result.PageNumOfResults) { PageResults = mappedResult };

            _logger.LogInformation("All Fixtures have been recieved successfully!!");

            return Ok(page);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetFixtureById(int id)
        {
            _logger.LogInformation($"Preparing to get fixture with id {id}...");

            var query = new GetFixtureById { FixtureId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"Fixture with id {id} was not found!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<FixtureGetDto>(result);

            _logger.LogInformation($"Fixture with id {id} has been recieved successfully!!!");

            return Ok(mappedResult);
        }

        [HttpPut]
        [Route("{fixtureId}")]
        [Authorize]
        public async Task<IActionResult> UpdateFixture(int fixtureId, [FromBody] FixturePutDto updated)
        {
            _logger.LogInformation($"Preparing to update fixture with id {fixtureId}...");

            var command = new UpdateFixture
            {
                FixtureId = fixtureId,
                newDate = updated.Date
            };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"Fixture with id {fixtureId} not found!!!");
                return NotFound();
            }

            _logger.LogInformation($"Fixture with id {fixtureId} updated successfully!!!");

            return NoContent();
        }
    }
}
