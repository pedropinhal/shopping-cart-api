using System;
using System.Threading.Tasks;
using Api.Client.Models;
using Newtonsoft.Json;

namespace Api.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new CartClient("https://localhost:5001");

            var cart = await client.Create();
            Console.WriteLine($"{JsonConvert.SerializeObject(cart)}");

            cart = await client.Get(cart.Id);
            Console.WriteLine($"{JsonConvert.SerializeObject(cart)}");

            cart = await client.AddProduct(cart.Id, new Product { ProductId = 1, Quantity = 2 });
            Console.WriteLine($"{JsonConvert.SerializeObject(cart)}");

            cart = await client.RemoveProduct(cart.Id, new Product { ProductId = 1, Quantity = 1 });
            Console.WriteLine($"{JsonConvert.SerializeObject(cart)}");

            cart = await client.Clear(cart.Id);
            Console.WriteLine($"{JsonConvert.SerializeObject(cart)}");
        }
    }
}
