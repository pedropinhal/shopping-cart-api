using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Api.Models;
using Api.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace Api.Tests
{
    public class WhenAddingAProduct : TestBase
    {
        private HttpResponseMessage _response;
        private Cart _model;
        private Product _product;
        private int _cartId = 1;

        [OneTimeSetUp]
        public async Task Setup()
        {
            var db = _api.Host.Services.GetRequiredService<IApiDbContext>();

            db.Carts.Add(new Cart { Id = _cartId });

            await db.SaveChangesAsync(CancellationToken.None);

            _product = new Product { Id = 1, Quantity = 1 };

            _response = await _client
                .PostAsync($"api/cart/{_cartId}",
                    Serialize(_product)
                );

            _model = await Deserialize<Cart>(_response);
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
            Assert.That(_model.Products[0].Id, Is.EqualTo(_product.Id));
            Assert.That(_model.Products[0].Quantity, Is.EqualTo(_product.Quantity));
        }
    }
}