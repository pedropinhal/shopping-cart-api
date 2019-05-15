using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Commands;
using Api.Concrete;
using Api.Interfaces;
using Api.Queries;
using Api.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CartModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create()
        {
            var cart = await _mediator.Send(new CreateCartCommand());
            return CreatedAtAction(nameof(Get), new { id = cart.Id }, cart);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CartModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var cart = await _mediator.Send(new GetCartQuery { Id = id });

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(typeof(ProductModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddProduct(int id, [FromBody] AddProductCommand command)
        {
            command.CartId = id;
            var cart = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = cart.Id }, cart);
        }

        [HttpDelete("{id}/products/{productId}/{quantity}")]
        [ProducesResponseType(typeof(CartModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveProduct(int id, int productId, int quantity = 1)
        {
            var command = new RemoveProductCommand { CartId = id, ProductId = productId, Quantity = quantity };
            var cart = await _mediator.Send(command);
            return Ok(cart);
        }

        [HttpPost("{id}/clear")]
        [ProducesResponseType(typeof(CartModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Clear(int id)
        {
            var cart = await _mediator.Send(new ClearCartCommand { CartId = id });

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }
    }
}
