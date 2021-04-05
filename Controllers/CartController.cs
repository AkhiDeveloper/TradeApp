using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TradeApp.Data;

namespace TradeApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }





        //Without Views Raw codes
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProduct(string productId)
        {
            //getting user Detail from https
            var userModel = await _userManager.GetUserAsync(User);

            //Checking cart existence
            if(CartExists(userModel.Id) == false)
            {
                _context.Carts.Add(new Cart()
                { CustomerId = userModel.Id });
            }

            //Finding Cart & product
            var cart = await _context.Carts.FindAsync(userModel.Id);
            var product = await _context.Product.FindAsync(productId);

            //Checking product
            if(product == null)
            {
                return NotFound();
            }

            //Finding Cartproduct
            var cartproduct = await _context.CartProducts
                .Include(x => x.Cart)
                .Where(x => x.Cart.CustomerId == userModel.Id)
                .FirstAsync(x => x.product == product);

            //Checking cartproduct
            if(cartproduct == null)
            {
                cartproduct = new CartProduct()
                {
                    Cart = cart,
                    AddedDate = DateTime.Now,
                    product = product,
                    quantity = 0

                };
                _context.CartProducts.Add(cartproduct);
                await _context.SaveChangesAsync();
            }

            //Adding Quantity
            cartproduct.quantity = cartproduct.quantity + 1;

            //Saving Data
            await _context.SaveChangesAsync();

            //Returning view to index page
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> AddProduct
            (string productId,int quantity)
        {
            //getting user Detail from https
            var userModel = await _userManager.FindByNameAsync
            (User.FindFirstValue(ClaimTypes.Name));

            //Checking cart existence
            if (CartExists(userModel.Id) == false)
            {
                _context.Carts.Add(new Cart()
                { CustomerId = userModel.Id });
            }

            //Finding Cart & product
            var cart = await _context.Carts.FindAsync(userModel.Id);
            var product = await _context.Product.FindAsync(productId);

            //Checking product
            if (product == null)
            {
                return NotFound();
            }

            //Finding Cartproduct
            var cartproduct = await _context.CartProducts
                .Include(x => x.Cart)
                .Where(x => x.Cart.CustomerId == userModel.Id)
                .FirstAsync(x => x.product == product);

            //Checking cartproduct
            if (cartproduct == null)
            {
                cartproduct = new CartProduct()
                {
                    Cart = cart,
                    AddedDate = DateTime.Now,
                    product = product,
                    quantity = 0

                };
                _context.CartProducts.Add(cartproduct);
                await _context.SaveChangesAsync();
            }

            //Adding Quantity
            cartproduct.quantity = cartproduct.quantity + quantity;

            //Saving Data
            await _context.SaveChangesAsync();

            //Returning view to index page
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeProductQuantity
            (string productId, decimal newquantity)
        {
            //getting user Detail from https
            var userModel = await _userManager.FindByNameAsync
            (User.FindFirstValue(ClaimTypes.Name));

            //Checking cart existence
            if (CartExists(userModel.Id) == false)
            {
                _context.Carts.Add(new Cart()
                { CustomerId = userModel.Id });
            }

            //Finding Cart & product
            var cart = await _context.Carts.FindAsync(userModel.Id);
            var product = await _context.Product.FindAsync(productId);

            //Checking product
            if (product == null)
            {
                return NotFound();
            }

            //Finding Cartproduct
            var cartproduct = await _context.CartProducts
                .Include(x => x.Cart)
                .Where(x => x.Cart.CustomerId == userModel.Id)
                .FirstAsync(x => x.product == product);

            //Checking cartproduct
            if (cartproduct == null)
            {
                cartproduct = new CartProduct()
                {
                    Cart = cart,
                    AddedDate = DateTime.Now,
                    product = product,
                    quantity = 0

                };
                _context.CartProducts.Add(cartproduct);
                await _context.SaveChangesAsync();
            }

            //Adding Quantity
            cartproduct.quantity = newquantity;

            //Saving Data
            await _context.SaveChangesAsync();

            //Returning view to index page
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveProduct(string productId)
        {
            //getting user Detail from https
            var userModel = await _userManager.FindByNameAsync
            (User.FindFirstValue(ClaimTypes.Name));

            //Checking cart existence
            if (CartExists(userModel.Id) == false)
            {
                _context.Carts.Add(new Cart()
                { CustomerId = userModel.Id });
            }

            //Finding Cart & product
            var cart = await _context.Carts.FindAsync(userModel.Id);
            var product = await _context.Product.FindAsync(productId);

            //Checking product
            if (product == null)
            {
                return NotFound();
            }

            //Finding Cartproduct
            var cartproduct = await _context.CartProducts
                .Include(x => x.Cart)
                .Where(x => x.Cart.CustomerId == userModel.Id)
                .FirstAsync(x => x.product == product);

            //Checking cartproduct
            if (cartproduct == null)
            {
                cartproduct = new CartProduct()
                {
                    Cart = cart,
                    AddedDate = DateTime.Now,
                    product = product,
                    quantity = 0

                };
                _context.CartProducts.Add(cartproduct);
                await _context.SaveChangesAsync();
            }

            //Adding Quantity
            cartproduct.quantity = cartproduct.quantity - 1;
            if(cartproduct.quantity <= 0)
            {
                _context.CartProducts.Remove(cartproduct);
            }

            //Saving Data
            await _context.SaveChangesAsync();

            //Returning view to index page
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReduceProduct
            (string productId, decimal quantity)
        {
            //getting user Detail from https
            var userModel = await _userManager.FindByNameAsync
            (User.FindFirstValue(ClaimTypes.Name));

            //Checking cart existence
            if (CartExists(userModel.Id) == false)
            {
                _context.Carts.Add(new Cart()
                { CustomerId = userModel.Id });
            }

            //Finding Cart & product
            var cart = await _context.Carts.FindAsync(userModel.Id);
            var product = await _context.Product.FindAsync(productId);

            //Checking product
            if (product == null)
            {
                return NotFound();
            }

            //Finding Cartproduct
            var cartproduct = await _context.CartProducts
                .Include(x => x.Cart)
                .Where(x => x.Cart.CustomerId == userModel.Id)
                .FirstAsync(x => x.product == product);

            //Checking cartproduct
            if (cartproduct == null)
            {
                cartproduct = new CartProduct()
                {
                    Cart = cart,
                    AddedDate = DateTime.Now,
                    product = product,
                    quantity = 0

                };
                _context.CartProducts.Add(cartproduct);
                await _context.SaveChangesAsync();
            }

            //Adding Quantity
            cartproduct.quantity = cartproduct.quantity - quantity;
            if (cartproduct.quantity <= 0)
            {
                _context.CartProducts.Remove(cartproduct);
            }

            //Saving Data
            await _context.SaveChangesAsync();

            //Returning view to index page
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProduct(string productid)
        {
            //getting user Detail from https
            var userModel = await _userManager.FindByNameAsync
            (User.FindFirstValue(ClaimTypes.Name));
            if(userModel == null)
            {
                return NotFound();
            }

            var cartproduct = await _context.CartProducts
                .Include(x => x.Cart)
                .Where(x => x.Cart.CustomerId == userModel.Id)
                .Include(x => x.product)
                .FirstAsync(x => x.product.Code == productid);

            if(cartproduct == null)
            {
                //Returning view to index page
                return RedirectToAction(nameof(Index));
            }

            _context.CartProducts.Remove(cartproduct);
            await _context.SaveChangesAsync();

            //Returning view to index page
            return RedirectToAction(nameof(Index));
        }

        private async Task<decimal?> TotalPrice()
        {
            //getting user Detail from https
            var userModel = await _userManager.FindByNameAsync
            (User.FindFirstValue(ClaimTypes.Name));
            if (userModel == null)
            {
                return null;
            }

            var cartProducts = _context.Carts
                .Include(x => x.cartProducts)
                .Single(x => x.CustomerId == userModel.Id)
                .cartProducts.ToList();
            decimal totalprice = 0;
            foreach(var cartproduct in cartProducts)
            {
                totalprice = totalprice + cartproduct.product.MRP;
            }
            return totalprice;
        }

        private async Task<decimal?> DiscountAmount ()
        {
            //getting user Detail from https
            var userModel = await _userManager.FindByNameAsync
            (User.FindFirstValue(ClaimTypes.Name));
            if (userModel == null)
            {
                return null;
            }

            var cartProducts = _context.Carts
                .Include(x => x.cartProducts)
                .Single(x => x.CustomerId == userModel.Id)
                .cartProducts.ToList();
            decimal discountprice = 0;
            foreach (var cartproduct in cartProducts)
            {
                discountprice = discountprice 
                    + cartproduct.product.MRP
                    *(1-cartproduct.product.Discount);
            }
            return discountprice;
        }

        private async Task<decimal?> NetPrice()
        {
            var totalprice = await TotalPrice();
            var discountamount = await DiscountAmount();
            return (totalprice - discountamount);
        }

        private bool CartExists(string customerid)
        {
            return _context.Carts.Any(e => e.CustomerId == customerid);
        }
    }
}
