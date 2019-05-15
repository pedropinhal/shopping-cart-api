using System.Threading.Tasks;
using Api.Client.Models;

namespace Api.Client
{
    public interface ICartClient
    {
        Task<Cart> Create();
        Task<Cart> Get(int cartId);
        Task<Cart> AddProduct(int cartId, Product product);
        Task<Cart> RemoveProduct(int cartId, Product product);
        Task<Cart> Clear(int cartId);
    }
}
