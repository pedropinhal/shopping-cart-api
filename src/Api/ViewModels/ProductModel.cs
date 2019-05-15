using System;
using System.Linq.Expressions;
using Api.DomainModels;

namespace Api.ViewModels
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public static Expression<Func<Product, ProductModel>> Projection
        {
            get
            {
                return product => new ProductModel
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                };
            }
        }

        public static ProductModel Create(Product product)
        {
            if (product == null)
                return null;

            return Projection.Compile().Invoke(product);
        }
    }
}