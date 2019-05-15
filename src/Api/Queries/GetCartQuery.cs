using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Queries
{
    public class GetCartQuery : IRequest<CartModel>
    {
        public int Id { get; set; }
    }

    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartModel>
    {
        private readonly IApiDbContext _apiDbContext;

        public GetCartQueryHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<CartModel> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            return await _apiDbContext
                .Carts
                .Select(CartModel.Projection)
                .Include(c => c.Products)
                .Where(c => c.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}