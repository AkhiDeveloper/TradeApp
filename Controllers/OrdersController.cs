using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TradeApp.Data;
using TradeApp.Models.Order;

namespace TradeApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            try
            {
                //getting user Detail from https
                var appuser = await _userManager.GetUserAsync(User);

                if(await _userManager.IsInRoleAsync(appuser,"Customer"))
                {
                    var ordersdata = await _context.Orders
                        .Include(x => x.orderproducts)
                        .ThenInclude(x=>x.product)
                        .Where(x => x.customerId == appuser.Id)
                        .ToListAsync();

                    var ordersmodel = _mapper.Map<IList<DetailVM>>(ordersdata);

                    return View(ordersmodel);
                }

                else if(await _userManager.IsInRoleAsync(appuser,"Admin"))
                {
                    var ordersdata = await _context.Orders
                        .Include(x => x.orderproducts)
                        .ThenInclude(x => x.product)
                        .ToListAsync();

                    var ordersmodel = _mapper.Map<IList<DetailVM>>(ordersdata);

                    return View(ordersmodel);
                }

                else return Redirect("Home");

            }
            catch(Exception e)
            {
                return Redirect("Home");
            }
            
        }

        [Authorize]
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(x => x.orderproducts)
                .ThenInclude(x => x.product)
                .FirstOrDefaultAsync(m => m.Code == id);
            if (order == null)
            {
                return NotFound();
            }

            var ordermodel = _mapper.Map<DetailVM>(order);

            return View(ordermodel);
        }

        
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> OrderCart()
        {
            //getting user Detail from https
            var appuser = await _userManager.GetUserAsync(User);
            if (appuser == null)
            {
                return NotFound();
            }

            //getting cartproducts
            var cartProductsdata = _context.Carts
                .Include(x => x.cartProducts)
                .ThenInclude(x=>x.product)
                .Single(x => x.CustomerId == appuser.Id)
                .cartProducts.ToList();

            //Mapping to orderproducts
            var orderproductsdata = _mapper.Map<ICollection<OrderProduct>>
                (cartProductsdata);

            //Creating oredr
            var order = new Order()
            {
                orderproducts = orderproductsdata,
                customerId = appuser.Id,                
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            //Deleting product from cart
            _context
                .Carts
                .Remove(await _context.Carts.FindAsync(appuser.Id));
            await _context.SaveChangesAsync();

            //Return to index page
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var appuser = await _userManager.GetUserAsync(User);

                var order = _context.Orders
                    .Where(x => x.customerId == appuser.Id)
                    .Where(x => x.IsConfirmed == false)
                    .Single(x => x.Code == id);

                _context.Remove(order);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch(ArgumentNullException arg)
            {
                return Redirect("Home");
            }
            
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ConfirmOrder(string id)
        {
            try
            {
                var order = _context.Orders
                    .Single(x => x.Code == id);

                order.IsConfirmed = true;
                _context.Update(order);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeliveredOrder(string id)
        {
            try
            {
                var order = _context.Orders
                    .Where(x=>x.IsConfirmed==true)
                    .Single(x => x.Code == id);

                order.IsDelivered = true;
                _context.Update(order);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.Code == id);
        }
    }
}
