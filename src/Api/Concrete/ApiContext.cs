using Api.DomainModels;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Concrete
{
    public class ApiDbContext : DbContext, IApiDbContext
    {
        public DbSet<Cart> Carts { get; set; }
        
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
    }
}