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
            CreateMap<CustomerProductRating, ViewModel.Product.CustomerProductRatingCreate>();

            CreateMap<Cart, Models.Cart.DetailVM>().ReverseMap();
            CreateMap<CartProduct, Models.Cart.CartProductVM>().ReverseMap();

            CreateMap<Order, Models.Order.DetailVM>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom<CustomResolver>());
            CreateMap<OrderProduct, Models.Order.OrderProductVM>()
                .ReverseMap();

            CreateMap<OrderProduct, CartProduct>().ReverseMap();
           
        }
    }
}
