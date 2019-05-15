using System.Threading;
using System.Threading.Tasks;
using Api.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Interfaces
{
    public interface IApiDbContext
    {
        DbSet<Cart> Carts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}