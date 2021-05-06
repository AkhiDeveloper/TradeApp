using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeApp.Data;

namespace TradeApp.CustomAuthorizations
{
    public class CustomerUsedProductRequriements
            : IAuthorizationRequirement
    {
        public CustomerUsedProductRequriements()
        {

        }
    }

    public class CustomerUsedProductHandler
        : AuthorizationHandler<CustomerUsedProductRequriements>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _datacontext;

        public CustomerUsedProductHandler
            (UserManager<IdentityUser> userManager,
            ApplicationDbContext datacontext,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userManager = userManager;
            _datacontext = datacontext;
            _httpContextAccessor = httpContextAccessor;
        }


        protected override Task HandleRequirementAsync
            (AuthorizationHandlerContext context, 
            CustomerUsedProductRequriements requirement)
        {
            try
            {
                var appuser = _userManager
                    .GetUserAsync(context.User).Result;



                //getting productid from httprequest
                var routeValue = _httpContextAccessor
                    .HttpContext
                    .Request
                    .RouteValues;
                object rawproductcode;
                routeValue.TryGetValue("id", out rawproductcode);
                if (rawproductcode == null)
                {
                    var querystring = _httpContextAccessor
                        .HttpContext.Request.Query;
                    rawproductcode = querystring["id"];
                }
                if (rawproductcode == null)
                {
                    return Task.CompletedTask;
                }

                var productcode = rawproductcode.ToString();

                
                    var orders = _datacontext.Orders.Where(x => x.customerId == appuser.Id).ToList();
                    //Checking product in order
                    foreach (var order in orders)
                    {
                        if (_datacontext.OrderProducts
                        .Where(x=>x.product.Code==productcode)
                        .Any(x => x.Order.Code == order.Code))
                        {
                            context.Succeed(requirement);
                        }
                    }
                

                return Task.CompletedTask;
            }
            catch
            {
                return Task.CompletedTask;
            }
            
        }
    }
}
