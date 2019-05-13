using System.Collections.Generic;

namespace Api.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public List<Product> Products { get; set; }
        
        public Cart()
        {
            Products = new List<Product>();
        }
    }
}