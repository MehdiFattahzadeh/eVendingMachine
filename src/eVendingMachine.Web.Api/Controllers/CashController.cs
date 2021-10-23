using eVendingMachine.Commands;
using eVendingMachine.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eVendingMachine.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CashController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCashCommand command)
            => Created(nameof(Post), await _mediator.Send(command));

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCashQuery query)
            => Ok(await _mediator.Send(query));

        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert([FromBody] InsertCasheToMachinCommand command)
            => Created(nameof(Post), await _mediator.Send(command));

    }
}
