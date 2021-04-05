using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TradeApp.Data;
using TradeApp.Models.Cart;

namespace TradeApp.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;

        public CartsController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Carts
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var appuser = await _userManager.GetUserAsync(User);
                
                if (!_context.Carts.Any(x => x.CustomerId == appuser.Id))
                {
                    CreateCart(appuser.Id);
                }

                var cartdata = _context.Carts
                    .Include(x => x.cartProducts)
                    .ThenInclude(x => x.product)
                    .Single(x => x.CustomerId == appuser.Id);

                var cartmodel = _mapper.Map<DetailVM>(cartdata);
                return View(cartmodel);
            }
            catch(Exception e)
            {
                return Redirect("Products/Index");
            }
           
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddProduct
            (string productId, int quantity=1)
        {
            try
            {
                //getting user Detail from https
                var appuser = await _userManager.GetUserAsync(User);

                //Checking cart existence
                if (CartsExists(appuser.Id) == false)
                {
                    CreateCart(appuser.Id);
                }

                //Finding Cart & product
                var cartdata = _context.Carts
                        .Include(x => x.cartProducts)
                        .ThenInclude(x => x.product)
                        .Single(x => x.CustomerId == appuser.Id);
                var productdata = await _context.Product.FindAsync(productId);

                //Checking product
                if (productdata == null)
                {
                    return Redirect("Products/Index");
                }
                
                //Checking cartproduct
                if (!cartdata.cartProducts.Any
                    (x => x.product.Code == productId))
                {
                    var newcartproductdata = new CartProduct()
                    {
                        Cart = cartdata,
                        AddedDate = DateTime.Now,
                        product = productdata,
                        quantity = 0

                    };
                    _context.CartProducts.Add(newcartproductdata);
                    await _context.SaveChangesAsync();
                }

                var cartproductdata = cartdata.cartProducts.Single
                    (x => x.product.Code == productId);

                //Adding Quantity
                cartproductdata.quantity = cartproductdata.quantity + quantity;

                //Saving Data
                await _context.SaveChangesAsync();

                //Returning view to index page
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                throw;
            }
            
        }


        private bool CartsExists(string customerid)
        {
            return _context.Carts.Any(e => e.CustomerId == customerid);
        }

        private void CreateCart(string customerid)
        {
            _context.Carts.Add(new Cart()
            {
                CustomerId = customerid
            }) ;
            _context.SaveChanges();
        }
    }
}
