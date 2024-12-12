using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Otus.Cqrs.Reader.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        //{
        //    var orderId = await _mediator.Send(command);
        //    return CreatedAtAction(nameof(GetOrderDetails), new { orderId }, null);
        //}

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var order = await _mediator.Send(new GetOrderDetailsQuery { OrderId = orderId });
            return Ok(order);
        }
    }

}
