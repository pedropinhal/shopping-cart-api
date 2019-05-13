using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Concrete
{
    public class ApiDbContext : DbContext, IApiDbContext
    {
        public DbSet<Cart> Carts { get; set; }
        //public DbSet<Product> Products { get; set; }
        
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
    }
}