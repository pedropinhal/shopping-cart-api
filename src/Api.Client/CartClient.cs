using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Api.Client.Models;
using Newtonsoft.Json;

namespace Api.Client
{
    public class CartClient : ICartClient
    {
        private static readonly HttpClient client = new HttpClient();

        public CartClient(string host)
        {
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Cart> AddProduct(int cartId, Product product)
        {
            var content = HttpContentExtensions.Serialize(product);
            HttpResponseMessage response = await client.PostAsync($"/api/cart/{cartId}", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsJsonAsync<Cart>();
        }

        public async Task<Cart> Clear(int cartId)
        {
            HttpResponseMessage response = await client.PostAsync($"/api/cart/{cartId}/clear", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsJsonAsync<Cart>();
        }

        public async Task<Cart> Create()
        {
            HttpResponseMessage response = await client.PostAsync("/api/cart", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsJsonAsync<Cart>();
        }

        public async Task<Cart> Get(int cartId)
        {
            HttpResponseMessage response = await client.GetAsync($"/api/cart/{cartId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsJsonAsync<Cart>();
        }

        public async Task<Cart> RemoveProduct(int cartId, Product product)
        {
            HttpResponseMessage response = await client.DeleteAsync($"/api/cart/{cartId}/products/{product.ProductId}/{product.Quantity}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsJsonAsync<Cart>();
        }
    }

    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }

        public static StringContent Serialize(object value)
        {
            var json = JsonConvert.SerializeObject(value);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
