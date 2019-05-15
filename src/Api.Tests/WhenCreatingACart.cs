using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.ViewModels;
using NUnit.Framework;

namespace Api.Tests
{
    public class WhenCreatingACart : TestBase
    {
        private HttpResponseMessage _response;
        private CartModel _model;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _response = await _api
                .CreateRequest("api/cart")
                .PostAsync();

            _model = await Deserialize<CartModel>(_response);
        }

        [Test]
        public void ShouldReturnFound()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void CartShouldBeCreated()
        {
            Assert.That(_model.Id, Is.GreaterThan(0));
            Assert.That(_model.Products, Is.Empty);
        }
    }
}