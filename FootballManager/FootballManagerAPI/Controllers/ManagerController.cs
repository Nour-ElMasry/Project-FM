using Application.Commands;
using Application.Queries;
using AutoMapper;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/v1/Managers")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;
        public readonly ILogger _logger;
        public ManagerController(IMapper mapper, IMediator mediator, ILogger<object> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation("Mananger Controller called...");
        }

        [HttpGet]
        [Route("All/{pg?}")]
        [Authorize]
        public async Task<IActionResult> GetAllManagers(int pg = 1)
        {
            _logger.LogInformation("Preparing to get all managers...");

            var result = await _mediator.Send(new GetAllManagers());

            if (result == null)
            {
                _logger.LogError("Couldn't get all managers!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<ManagerGetDto>>(result);

            _logger.LogInformation("All managers received successfully!!!");

            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetManagerById(int id)
        {
            _logger.LogInformation($"Preparing to get manager with id {id}...");

            var query = new GetManagerById { ManagerId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogError($"Manager with id {id} not found!!!");
                return NotFound();
            }

            var mappedResult = _mapper.Map<ManagerGetDto>(result);

            _logger.LogInformation($"Manager with id {id} received successfully!!!");

            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteManager(int id)
        {
            _logger.LogInformation($"Preparing to delete manager with id {id}...");

            var command = new DeleteManager { ManagerId = id };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError($"Manager with id {id} not found!!!");
                return NotFound();
            }

            _logger.LogInformation($"Manager with id {id} deleted successfully!!!");

            return NoContent();
        }
    }
}
