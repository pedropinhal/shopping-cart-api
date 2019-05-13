using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Queries
{
    public class GetCartQuery : IRequest<Cart>
    {
        public int Id { get; set; }
    }

    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, Cart>
    {
        private readonly IApiDbContext _apiDbContext;

        public GetCartQueryHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<Cart> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            return await _apiDbContext
                .Carts
                .Include(c => c.Products)
                .Where(c => c.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}