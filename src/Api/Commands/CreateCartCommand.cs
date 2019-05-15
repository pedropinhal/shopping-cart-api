using System.Threading;
using System.Threading.Tasks;
using Api.DomainModels;
using Api.Interfaces;
using Api.ViewModels;
using MediatR;

namespace Api.Commands
{
    public class CreateCartCommand : IRequest<CartModel>
    {
    }

    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, CartModel>
    {
        private readonly IApiDbContext _apiDbContext;

        public CreateCartCommandHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<CartModel> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new Cart();

            _apiDbContext.Carts.Add(cart);
            
            await _apiDbContext.SaveChangesAsync(cancellationToken);

            return CartModel.Create(cart);
        }
    }
}