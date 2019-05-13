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
    public class WhenGettingACart : TestBase
    {
        private HttpResponseMessage _response;
        private Cart _model;
        private int _cartId = 1;

        [OneTimeSetUp]
        public async Task Setup()
        {
            var db = _api.Host.Services.GetRequiredService<IApiDbContext>();

            db.Carts.Add(new Cart { Id = _cartId });

            await db.SaveChangesAsync(CancellationToken.None);

            _response = await _api
                .CreateRequest($"api/cart/{_cartId}")
                .GetAsync();

            _model = await Deserialize<Cart>(_response);
        }

        [Test]
        public void ShouldReturnFound()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CartShouldExist()
        {
            Assert.That(_model.Id, Is.EqualTo(_cartId));
        }
    }
}