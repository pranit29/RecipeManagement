using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeOnlineIngredientSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace RecipeOnlineIngredientSystem.Controllers
{
    public class AdminController : Controller
    {
        private OnlineRecipe1Context _db;
        private IHostingEnvironment _he;

        public AdminController(OnlineRecipe1Context db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.Recipes.ToList());
        }
        //1.Recipe
        //GET Create Action Method

        public ActionResult Create()
        {
            return View();
        }

        //POST Create Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recipe res, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                _db.Recipes.Add(res);
                await _db.SaveChangesAsync();
                TempData["save"] = "Recipe has been saved";
                return RedirectToAction(nameof(Index));

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    res.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    res.Image = "Images/Nature.jpg";
                }
            }

            return View(res);
        }

        //GET Edit Action Method

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _db.Recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Recipe recipe, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    recipe.Image = "Images/" + image.FileName;
                }

                //if (image == null)
                //{
                //    recipe.Image = "Images/Nature.jpg";
                //}
                _db.Recipes.Update(recipe);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(recipe);
        }

        //GET Details Action Method

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _db.Recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }



        //GET Delete Action Method

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var product = _db.Products.Include(c => c.SpecialTag).Include(c => c.ProductTypes).Where(c => c.Id == id).FirstOrDefault();
            var recipe = _db.Recipes.FirstOrDefault(c => c.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
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

            var recipe = _db.Recipes.FirstOrDefault(c => c.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            _db.Recipes.Remove(recipe);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
    //Product



}

