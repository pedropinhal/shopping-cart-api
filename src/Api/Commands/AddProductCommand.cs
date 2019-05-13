using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Commands
{
    public class AddProductCommand : IRequest<Cart>
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Cart>
    {
        private readonly IApiDbContext _apiDbContext;

        public AddProductCommandHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<Cart> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product { Id = request.ProductId, Quantity = request.Quantity };

            var cart = await _apiDbContext
                .Carts
                .Include(c => c.Products)
                .Where(c => c.Id == request.CartId).SingleOrDefaultAsync(cancellationToken);
            
            var existingProduct = cart.Products.SingleOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Quantity += product.Quantity;
            }
            else
            {
                cart.Products.Add(product);
            }

            await _apiDbContext.SaveChangesAsync(cancellationToken);

            return cart;
        }
    }
}