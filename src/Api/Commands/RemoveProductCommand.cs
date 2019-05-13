using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Commands
{
    public class RemoveProductCommand : IRequest<Cart>
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, Cart>
    {
        private readonly IApiDbContext _apiDbContext;

        public RemoveProductCommandHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<Cart> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
           var product = new Product { Id = request.ProductId, Quantity = request.Quantity };

            var cart = await _apiDbContext
                .Carts
                .Include(c => c.Products)
                .Where(c => c.Id == request.CartId).SingleOrDefaultAsync(cancellationToken);

            var existingProduct = cart.Products.SingleOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Quantity -= product.Quantity;
                if (existingProduct.Quantity <= 0)
                {
                    cart.Products.Clear();
                }
            }

            await _apiDbContext.SaveChangesAsync(cancellationToken);

            return cart;
        }
    }
}