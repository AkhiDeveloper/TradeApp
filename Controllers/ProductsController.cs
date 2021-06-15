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
using AdminProductModel = TradeApp.Models.Product.Admin;
using TradeApp.Models.Product;

namespace TradeApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;

        public ProductsController
            (ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        // GET: Products
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var productdata = _context.Product;
            string adminrole = "Admin";
            var appuser = _userManager.GetUserAsync(User).Result;
            if (_signInManager.IsSignedIn(User) &&
                _userManager.IsInRoleAsync(appuser, adminrole).Result)
            {
                var productdetailsmodel = _mapper.Map
                    <List<Product>,
                    List<Models.Product.Admin.DetailVM>>(await productdata.ToListAsync());
                return View("AdminIndex", productdetailsmodel);
            }

            var productsmodel = _mapper.Map<List<DetailVM>>(await productdata.ToListAsync());
            return View(productsmodel);
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Code == id);
            if (product == null)
            {
                return NotFound();
            }

            string adminrole = "Admin";
            if (_signInManager.IsSignedIn(User) && User.IsInRole(adminrole))
            {
                var productdetailmodel = _mapper.Map<AdminProductModel.DetailVM>(product);
                return View("AdminDetails", productdetailmodel);
            }

            return View(_mapper.Map<DetailVM>(product));
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
            ([Bind("Code,ProductName,BrandName,MRP,Discount")]
              AdminProductModel.CreateVM productmodel)
        {
            var productdata = _mapper.Map<Product>(productmodel);
            if (ModelState.IsValid)
            {
                _context.Add(productdata);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productmodel);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productmodel = _mapper.Map<AdminProductModel.UpdateVM>(product);
            return View(productmodel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductName,BrandName,MRP,Discount")] AdminProductModel.UpdateVM producmodel)
        {

            if (ModelState.IsValid)
            {
                var productdata = _context.Product.FindAsync(id).Result;
                try
                {
                    _mapper.Map(producmodel, productdata);
                    _context.Update(productdata);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(productdata.Code))
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
            return View(producmodel);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Code == id);
            if (product == null)
            {
                return NotFound();
            }

            var productmodel = _mapper.Map<AdminProductModel.DetailVM>(product);

            return View(productmodel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RateProduct
            (string id)
        {
            try
            {
                //Getting Userdata
                var appuser = await _userManager.GetUserAsync(User);

                //Checking Customer Product Rating in database
                if (!_context.CustomerProductRatings.Any(x => x.productcode == id))
                {
                    //Creating Customer Product Rating
                    var productratingmodel = new CustomerProductRating()
                    {
                                productcode = id,
                                customerid = appuser.Id
                    };
                            await _context.AddAsync(productratingmodel);
                            await _context.SaveChangesAsync();
                }
                var productratingview = _mapper.Map<ViewModel.Product.CustomerProductRatingCreate>
                    (await _context.CustomerProductRatings.SingleAsync(x=>x.productcode==id));
                
                //Redirect to View to Enter rating
                return View(productratingview);
            }
            catch
            {
                return RedirectToAction(nameof(Controllers.HomeController.Index),
                   "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RateProduct(ViewModel.Product.CustomerProductRatingCreate customerProductRatingupdate)
        {
            try
            {
                //Getting Userdata
                var appuser = await _userManager.GetUserAsync(User);

                //Finding Customer Product Rating
                var customerProductRating = await _context.CustomerProductRatings
                    .Include(x => x.customer)
                    .SingleAsync(x => x.customer.Id == appuser.Id);

                _mapper.Map(customerProductRatingupdate, customerProductRating);

                _context.Update(customerProductRating);
                await _context.SaveChangesAsync();

                return Redirect("Orders");
            }
            catch
            {
                return RedirectToAction(nameof(RateProduct), customerProductRatingupdate.Id);
            }
            
        }

        private bool ProductExists(string id)
        {
            return _context.Product.Any(e => e.Code == id);
        }
    }
}
