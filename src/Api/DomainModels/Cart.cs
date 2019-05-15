using System.Collections.Generic;

namespace Api.DomainModels
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