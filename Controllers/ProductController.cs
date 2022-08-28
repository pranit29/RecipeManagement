using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeOnlineIngredientSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace RecipeOnlineIngredientSystem.Controllers
{
    public class ProductController : Controller
    {
        private OnlineRecipe1Context _db;
        private IHostingEnvironment _he;

        public ProductController(OnlineRecipe1Context db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }

        public IActionResult Index()
        {
            return View(_db.ProductDetails.Include(c => c.Recipe).ToList());
        }

        //Get Create method
        public IActionResult Create()
        {
            ViewData["RecipeId"] = new SelectList(_db.Recipes.ToList(), "Id", "RecipeName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDetail product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _db.ProductDetails.FirstOrDefault(c => c.Name == product.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["RecipeId"] = new SelectList(_db.Recipes.ToList(), "Id", "RecipeName");
                    // ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
                    return View(product);
                }

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                //if (image == null)
                //{
                //    product.Image = "Images/Nature.jpg";
                //}
                _db.ProductDetails.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }
        //GET Edit Action Method

        public ActionResult Edit(int? id)
        {
            ViewData["RecipeId"] = new SelectList(_db.Recipes.ToList(), "Id", "RecipeName");
            //ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
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

        //POST Edit Action Method
        [HttpPost]
        public async Task<IActionResult> Edit(ProductDetail products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }

                _db.ProductDetails.Update(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(products);
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

        //GET Delete Action Method

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = _db.Products.Include(c => c.SpecialTag).Include(c => c.ProductTypes).Where(c => c.Id == id).FirstOrDefault();
            var product = _db.ProductDetails.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST Delete Action Method

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.ProductDetails.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _db.ProductDetails.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




    }
}
