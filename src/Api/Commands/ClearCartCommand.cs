using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Commands
{
    public class ClearCartCommand : IRequest<CartModel>
    {
        public int CartId { get; set; }
    }

    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, CartModel>
    {
        private readonly IApiDbContext _apiDbContext;

        public ClearCartCommandHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<CartModel> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _apiDbContext
               .Carts
               .Include(c => c.Products)
               .Where(c => c.Id == request.CartId)
               .SingleOrDefaultAsync(cancellationToken);

            if (cart == null)
            {
                return null;
            }

            cart.Products.Clear();

            await _apiDbContext.SaveChangesAsync(cancellationToken);

            return CartModel.Create(cart);
        }
    }
}