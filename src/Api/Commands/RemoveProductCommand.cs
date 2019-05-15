using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.DomainModels;
using Api.Interfaces;
using Api.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Commands
{
    public class RemoveProductCommand : IRequest<CartModel>
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, CartModel>
    {
        private readonly IApiDbContext _apiDbContext;

        public RemoveProductCommandHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<CartModel> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product { ProductId = request.ProductId, Quantity = request.Quantity };

            var cart = await _apiDbContext
                .Carts
                .Include(c => c.Products)
                .Where(c => c.Id == request.CartId).SingleOrDefaultAsync(cancellationToken);

            if (cart == null)
            {
                return null;
            }

            var existingProduct = cart.Products.SingleOrDefault(p => p.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.Quantity -= product.Quantity;
                if (existingProduct.Quantity <= 0)
                {
                    cart.Products.Clear();
                }
            }

            await _apiDbContext.SaveChangesAsync(cancellationToken);

            return CartModel.Create(cart);
        }
    }
}