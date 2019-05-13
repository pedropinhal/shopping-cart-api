using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Interfaces
{
    public interface IApiDbContext
    {
        DbSet<Cart> Carts { get; set; }
        // DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}