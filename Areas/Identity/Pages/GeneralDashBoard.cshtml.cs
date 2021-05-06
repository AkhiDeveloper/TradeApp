using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TradeApp.Areas.Identity.Pages
{
    
    public class GeneralDashBoardModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Data.ApplicationDbContext _dbContext;
        private readonly IMapper _mapper; 

        public GeneralDashBoardModel(
            UserManager<IdentityUser> userManager,
            Data.ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IList<Models.Product.DetailVM> productDetails { get; set; }

        public IList<Models.Order.DetailVM> orderDetails { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var appuser = await _userManager.GetUserAsync(User);

                var products = _dbContext.Product.ToList();

                productDetails = _mapper.Map<IList<Models.Product.DetailVM>>(products);

                var userorders = _dbContext.Orders
                    .Where(x => x.customerId == appuser.Id)
                    .ToList();

                orderDetails = _mapper.Map<IList<Models.Order.DetailVM>>(userorders);



                return Page();
            }
            catch
            {
                return Page();  
            }
            
        }
    }
}