using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeApp.Data;
using TradeApp.Models.Order;

namespace TradeApp.Mappings
{
    public class CustomResolver : IValueResolver<Data.Order, Models.Order.DetailVM, string>
    {

        public string Resolve(Order source, DetailVM destination, string destMember, ResolutionContext context)
        {
            if (source.IsConfirmed == false)
            {
                return "Not Confirmed";
            }
            else
            {
                if (source.IsDelivered == false)
                {
                    return "Confirmed";
                }
                else
                {
                    return "Delivered";
                }
            }
        }
    }

}
