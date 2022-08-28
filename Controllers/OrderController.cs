using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeOnlineIngredientSystem.Data;
using RecipeOnlineIngredientSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Customer.Controllers
{
    // [Area("Customer")]
    public class OrderController : Controller
    {

        private OnlineRecipe1Context _db;

        public OrderController(OnlineRecipe1Context db)
        {
            _db = db;
        }

        //GET Checkout actioin method
        [HttpPost]
        public ActionResult Checkout()
        {

            return View();
        }

        //  POST Checkout action method

        //[HttpPost]
        // [ValidateAntiForgeryToken]

        //public async Task<IActionResult> Checkout(Order anOrder)
        //{
        //    List<ProductDetails> products = HttpContext.Session.Get<List<ProductDetails>>("products");
        //    if (products != null)
        //    {
        //        foreach (var product in products)
        //        {
        //            OrderDetail orderDetails = new OrderDetail();
        //            orderDetails.ProductId = product.Id;
        //            anOrder.OrderDetails.Add(orderDetails);

        //        }
        //    }

        //    anOrder.OrderNo = GetOrderNo();
        //    _db.Orders.Add(anOrder);
        //    await _db.SaveChangesAsync();
        //    HttpContext.Session.Set("products", new List<ProductDetails>());
        //    return View();
        //}


        //public int GetOrderNo()
        //{
        //    int rowCount = _db.Orders.ToList().Count() + 1;
        //    return rowCount;
        //}















        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Order ord)
        {
            List<ProductDetail> products = HttpContext.Session.Get<List<ProductDetail>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderDetail orderDetails = new OrderDetail();
                    orderDetails.ProductId = product.Id;
                    ord.OrderDetails.Add(orderDetails);
                    _db.SaveChanges();

                }
            }

            Order o = new Order()
            {
                Id = ord.Id,
                Name = ord.Name,
                PhoneNo = ord.PhoneNo,
                Email = ord.Email,
                Address = ord.Address,
                OrderDate = System.DateTime.Today,
                ShippingDate = System.DateTime.Today.AddDays(4)
            };


            _db.Orders.Add(o);
            _db.SaveChanges();
            return View();

        }



        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
