using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Api.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Api.ViewModels;
using Api.DomainModels;

namespace Api.Tests
{
    public class WhenRemovingAProduct : TestBase
    {
        private HttpResponseMessage _response;
        private CartModel _model;
        private Product _product = new Product { ProductId = 1, Quantity = 1 };
        private int _cartId = 1;

        [OneTimeSetUp]
        public async Task Setup()
        {
            var db = _api.Host.Services.GetRequiredService<IApiDbContext>();

            var cart = new Cart { Id = _cartId };
            cart.Products.Add(_product);
            db.Carts.Add(cart);

            await db.SaveChangesAsync(CancellationToken.None);
            
            _response = await _client
                .DeleteAsync($"api/cart/{_cartId}/products/{_product.ProductId}/{_product.Quantity}");

            _model = await Deserialize<CartModel>(_response);
        }

        [Test]
        public void ShouldSucceed()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void ProductShouldBeAddedToCart()
        {
            Assert.That(_model.Products.Count, Is.EqualTo(0));
        }
    }
}