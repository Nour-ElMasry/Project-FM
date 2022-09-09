using Application.Commands;
using Application.Queries;
using AutoMapper;
using FootballManagerAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballManagerAPI.Controllers
{
    [Route("api/v1/Managers")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly IMediator _mediator;

        public ManagerController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManagers()
        {
            var result = await _mediator.Send(new GetAllManagers());;
            var mappedResult = _mapper.Map<List<ManagerGetDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetManagerById(int id)
        {
            var query = new GetManagerById { ManagerId = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            var mappedResult = _mapper.Map<ManagerGetDto>(result);
            return Ok(mappedResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var command = new DeleteManager { ManagerId = id };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
