//MVC CRUD

using Microsoft.AspNetCore.Mvc;
using MVC_PRODUCT.Models;
using MVC_PRODUCT.Services;

namespace MVC_PRODUCT.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _services;
        public ProductController(IProductServices services)
        {
            _services = services;
        }

        public IActionResult Index(string? search, string? sort, decimal? minPrice, decimal? maxPrice, int page = 1, int pageSize = 3  )
        {
            var result = _services.GetAllProducts(search, sort, minPrice, maxPrice, page, pageSize);
            if (!result.IsSuccess)
            {
                ViewBag.ErrorMessage = result.Message;
                return View(new List<Product>());
            }
            var totalItems = _services.GetAllProducts(
    search, sort, minPrice, maxPrice, 1, int.MaxValue
).Data.Count();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.Page = page;
            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product prd)
        {
            if (!ModelState.IsValid)
            {
                return View(prd);
            }
            var res = _services.Add(prd);
            if (!res.IsSuccess)
            {
                ViewBag.ErrorMessage = res.Message;
                return View(prd);
            }
            if (res.IsSuccess)
            {
                TempData["Success"] = "Product added successfully";


            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var res = _services.GetById(id);
            if (!res.IsSuccess)
            {
                ViewBag.ErrorMessage = res.Message;
                return RedirectToAction("Index");
            }
            return View(res.Data);
        }

        [HttpPost]
        public IActionResult Edit(int Id,Product prd)
        {
            if (!ModelState.IsValid)
            {
                return View(prd);
            }
            var res=_services.Update(Id, prd);
            if (!res.IsSuccess)
            {
                 ViewBag.ErrorMessage = res.Message;
                return View(prd);
            }
            if (res.IsSuccess)
            {
                TempData["Success"] = "Product edited successfully";

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete (int Id)
        {
            var res = _services.Delete(Id);
            TempData["Success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
