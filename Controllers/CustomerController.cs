using RecipeOnlineIngredientSystem.Models;
using RecipeOnlineIngredientSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using X.PagedList;

namespace RecipeOnlineIngredientSystem.Controllers
{
    //[Area("Customer")]
    public class CustomerController : Controller
    {
        private OnlineRecipe1Context _db;


        public CustomerController(OnlineRecipe1Context db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            return View(_db.ProductDetails.Include(c => c.Recipe).ToList());
        }

        /*private IActionResult View(object p)
        {
            throw new NotImplementedException();
        }
        */
        public IActionResult Privacy()
        {
            return View();
        }
        //GET Details Action Method
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.ProductDetails.Include(c => c.Recipe)
                .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {
            List<ProductDetail> prod = new List<ProductDetail>();
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.ProductDetails.Include(c => c.Recipe).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            prod = HttpContext.Session.Get<List<ProductDetail>>("products");
            if (prod == null)
            {
                prod = new List<ProductDetail>();
            }
            prod.Add(product);
            HttpContext.Session.Set<List<ProductDetail>>("products", prod);
            return RedirectToAction(nameof(Cart));
        }

        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<ProductDetail> products = HttpContext.Session.Get<List<ProductDetail>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public IActionResult Remove(int? id)
        {
            List<ProductDetail> products = HttpContext.Session.Get<List<ProductDetail>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //  GET product Cart action method

        public IActionResult Cart()
        {

            List<ProductDetail> prod = HttpContext.Session.Get<List<ProductDetail>>("products");
            if (prod == null)
            {
                prod = new List<ProductDetail>();
            }
            return View(prod);
        }


       // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}

