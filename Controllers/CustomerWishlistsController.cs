using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TradeApp.Data;

namespace TradeApp.Controllers
{
    public class CustomerWishlistsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerWishlistsController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CustomerWishlists
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //getting user Detail from https
            var userModel = await _userManager.FindByNameAsync
            (User.FindFirstValue(ClaimTypes.Name));

            //returning logged in user wishlists
            return View(await _context.CustomerWishlists
                .Where(x => x.customer.Id == userModel.Id).ToListAsync());
        }

        [Authorize]
        // GET: CustomerWishlists/Details/5
        public async Task<IActionResult> Details(string id)
        {
            //checking id state
            if (id == null)
            {
                return NotFound();
            }

            // getting Customerlists with products
            var customerWishlist = await _context.CustomerWishlists
                .Include(x => x.products)
                .FirstOrDefaultAsync(m => m.Code == id);

            //checking list state
            if (customerWishlist == null)
            {
                return NotFound();
            }

            //returning view
            return View(customerWishlist);
        }

        // GET: CustomerWishlists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerWishlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name")] CustomerWishlist customerWishlist)
        {

            if (ModelState.IsValid)
            {
                //getting user Detail from https
                var userModel = await _userManager.FindByNameAsync
                (User.FindFirstValue(ClaimTypes.Name));

                //Asigning User as customer
                customerWishlist.customer.Id = userModel.Id;

                //Writing to database
                _context.Add(customerWishlist);
                await _context.SaveChangesAsync();

                //returning View
                return RedirectToAction(nameof(Index));
            }

            return View(customerWishlist);
        }

        // GET: CustomerWishlists/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerWishlist = await _context.CustomerWishlists.FindAsync(id);
            if (customerWishlist == null)
            {
                return NotFound();
            }
            return View(customerWishlist);
        }

        // POST: CustomerWishlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name")] CustomerWishlist customerWishlist)
        {
            if (id != customerWishlist.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerWishlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerWishlistExists(customerWishlist.Code))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customerWishlist);
        }

        // GET: CustomerWishlists/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerWishlist = await _context.CustomerWishlists
                .FirstOrDefaultAsync(m => m.Code == id);
            if (customerWishlist == null)
            {
                return NotFound();
            }

            return View(customerWishlist);
        }

        // POST: CustomerWishlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customerWishlist = await _context.CustomerWishlists.FindAsync(id);
            _context.CustomerWishlists.Remove(customerWishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Post: CustomerWishlist/AddProduct
        public async Task<IActionResult> AddProduct()
        {
            return RedirectToAction("Index","Products");
        }
        public async Task<IActionResult> AddProduct(string productid)
        {
            //getting user Detail from https
            var userModel = await _userManager.FindByNameAsync
            (User.FindFirstValue(ClaimTypes.Name));

            //returning logged in user wishlists
            return View(await _context.CustomerWishlists
                .Where(x => x.customer.Id == userModel.Id).ToListAsync());
        }
        //Post: CustomerWishlist/AddProduct/5
        [HttpPost]
        public async Task<IActionResult> AddProduct(string id, string productId)
        {

            //Finding wishlist
            var wishlisttask = _context.CustomerWishlists.FindAsync(id);
            //Finding Product
            var producttask = _context.Product.FindAsync(productId);

            var wishlist = wishlisttask.GetAwaiter().GetResult();
            var product = producttask.GetAwaiter().GetResult();

            if (wishlisttask.GetAwaiter().GetResult() == null
                && producttask.GetAwaiter().GetResult() == null)
            {
                return NotFound();
            }

            //Adding product to wishlist
            wishlist.products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details));
        }

        //Post: CustomerWishlist/AddProduct/5
        public async Task<IActionResult> ProductDetail(string id)
        {

            //Finding wishlist
            var wishlisttask = _context.CustomerWishlists
                .Include(x => x.products)
                .FirstAsync(x =>x.Code == id);

            if (wishlisttask.GetAwaiter().GetResult() == null)
            {
                return NotFound();
            }

            //returning logged in user wishlists
            return View(wishlisttask.GetAwaiter().GetResult());
        }

        //Post: CustomerWishlist/AddProduct/5
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string id, string productId)
        {

            //Finding wishlist
            var wishlisttask = _context.CustomerWishlists.FindAsync(id);
            //Finding Product
            var producttask = _context.Product.FindAsync(productId);

            var wishlist = wishlisttask.GetAwaiter().GetResult();
            var product = producttask.GetAwaiter().GetResult();

            if (wishlisttask.GetAwaiter().GetResult() == null
                && producttask.GetAwaiter().GetResult() == null)
            {
                return NotFound();
            }

            //Adding product to wishlist
            wishlist.products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details));
        }

        private bool CustomerWishlistExists(string id)
        {
            return _context.CustomerWishlists.Any(e => e.Code == id);
        }
    }
}
