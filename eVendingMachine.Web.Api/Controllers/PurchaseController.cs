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
    public class PurchaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PurchaseController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("Cancel")]
        public async Task<IActionResult> Cancel([FromBody] CancelPurchaseCommand command)
                => Ok(await _mediator.Send(command));

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PickProductCommand command)
            => Ok(await _mediator.Send(command));
    }
}
