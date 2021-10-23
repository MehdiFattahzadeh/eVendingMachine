using eVendingMachine.Commands;
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
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CurrencyController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCurrencyCommand command)
            => Created(nameof(Post), await _mediator.Send(command));
    }
}
