using AutoMapper;
using TradeApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TradeApp.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Product, Models.Product.Admin.CreateVM>().ReverseMap();
            CreateMap<Product, Models.Product.Admin.DetailVM>().ReverseMap();
            CreateMap<Product, Models.Product.Admin.UpdateVM>().ReverseMap();
            CreateMap<Product, Models.Product.DetailVM>().ReverseMap();

            CreateMap<Cart, Models.Cart.DetailVM>().ReverseMap();
            CreateMap<CartProduct, Models.Cart.CartProductVM>().ReverseMap();
           
        }
    }
}
