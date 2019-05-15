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
    public class WhenAddingAProduct : TestBase
    {
        private HttpResponseMessage _response;
        private CartModel _model;
        private ProductModel _product;
        private int _cartId = 1;

        [OneTimeSetUp]
        public async Task Setup()
        {
            var db = _api.Host.Services.GetRequiredService<IApiDbContext>();
            db.Carts.Add(new Cart { Id = _cartId });
            await db.SaveChangesAsync(CancellationToken.None);

            _product = new ProductModel { ProductId = 1, Quantity = 1 };
            _response = await _client
                .PostAsync($"api/cart/{_cartId}",
                    Serialize(_product)
                );

            _model = await Deserialize<CartModel>(_response);
        }

        [Test]
        public void ShouldSucceed()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void ProductShouldBeAddedToCart()
        {
            Assert.That(_model.Products.Count, Is.EqualTo(1));
            Assert.That(_model.Products[0].ProductId, Is.EqualTo(_product.ProductId));
            Assert.That(_model.Products[0].Quantity, Is.EqualTo(_product.Quantity));
        }
    }
}