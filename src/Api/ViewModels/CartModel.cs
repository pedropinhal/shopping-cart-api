using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Api.DomainModels;

namespace Api.ViewModels
{
    public class CartModel
    {
        public int Id { get; set; }

        public List<ProductModel> Products { get; set; }

        public CartModel()
        {
            Products = new List<ProductModel>();
        }

        public static Expression<Func<Cart, CartModel>> Projection
        {
            get
            {
                return cart => new CartModel
                {
                    Id = cart.Id,
                    Products = cart.Products.AsQueryable()
                        .Select(ProductModel.Projection)
                        .ToList()
                };
            }
        }

        public static CartModel Create(Cart cart)
        {
            if (cart == null)
                return null;

            return Projection.Compile().Invoke(cart);
        }
    }
}