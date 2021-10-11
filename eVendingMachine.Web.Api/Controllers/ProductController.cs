using eVendingMachine.Commands;
using eVendingMachine.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eVendingMachine.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProductCommand command)
            => Created(nameof(Post), await _mediator.Send(command));

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProductQuery query)
             => Ok(await _mediator.Send(query));
    }
}
