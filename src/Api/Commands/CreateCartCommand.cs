using System.Threading;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models;
using MediatR;

namespace Api.Commands
{
    public class CreateCartCommand : IRequest<Cart>
    {

    }

    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, Cart>
    {
        private readonly IApiDbContext _apiDbContext;

        public CreateCartCommandHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<Cart> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new Cart();

            _apiDbContext.Carts.Add(cart);
            
            await _apiDbContext.SaveChangesAsync(cancellationToken);

            return cart;
        }
    }
}