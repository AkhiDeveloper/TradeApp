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

namespace TradeApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public OrdersController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize]
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            //getting user Detail from https
            var userModel = await _userManager.FindByNameAsync
            (User.FindFirstValue(ClaimTypes.Name));
            if (userModel == null)
            {
                return NotFound();
            }

            return View(await _context.Orders
                .Where(x => x.customer.Id == userModel.Id)
                .ToListAsync());
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
                .FirstOrDefaultAsync(m => m.Code == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        
        [Authorize]
        public async Task<IActionResult> OrderCart()
        {
            //getting user Detail from https
            var userModel = await _userManager.FindByNameAsync
            (User.FindFirstValue(ClaimTypes.Name));
            if (userModel == null)
            {
                return NotFound();
            }

            //getting cartproducts
            var cartProducts = _context.Carts
                .Include(x => x.cartProducts)
                .Single(x => x.CustomerId == userModel.Id)
                .cartProducts.ToList();

            //Mapping to orderproducts
            var orderproducts = _mapper.Map<IEnumerable<OrderProduct>>
                (cartProducts);

            //Creating oredr
            var order = new Order()
            {
                orderproducts = new List<OrderProduct>()
            };
            order.orderproducts
                .ToList()
                .AddRange(orderproducts);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            //Deleting product from cart
            _context
                .Carts
                .Remove(await _context.Carts.FindAsync(userModel.Id));

            //Return to index page
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.Code == id);
        }
    }
}
