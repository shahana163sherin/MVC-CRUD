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
        //inmemory
        //        public IActionResult Index(string? search, string? sort, decimal? minPrice, decimal? maxPrice, int page = 1, int pageSize = 3  )
        //        {
        //            var result = _services.GetAllProducts(search, sort, minPrice, maxPrice, page, pageSize);
        //            if (!result.IsSuccess)
        //            {
        //                ViewBag.ErrorMessage = result.Message;
        //                return View(new List<Product>());
        //            }
        //            var totalItems = _services.GetAllProducts(
        //    search, sort, minPrice, maxPrice, 1, int.MaxValue
        //).Data.Count();

        //            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        //            ViewBag.Page = page;
        //            return View(result.Data);
        //        }

        public async Task<IActionResult> Index(string? search, string? sort, decimal? minPrice, decimal? maxPrice, int page = 1, int pageSize = 3)
        {
            var data = await _services.GetAllProductsAsync(search, sort, minPrice, maxPrice, page, pageSize);
            if (!data.IsSuccess)
            {
                ViewBag.ErrorMessage = data.Message;
                return View(new List<Product>());
            }
            var totalItems = data.Data.TotalItems;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.TotalPage = totalPages;
            ViewBag.Page = page;
            return View(data.Data.Data);
        }

        //public IActionResult Create()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult Create(Product prd)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(prd);
        //    }
        //    var res = _services.Add(prd);
        //    if (!res.IsSuccess)
        //    {
        //        ViewBag.ErrorMessage = res.Message;
        //        return View(prd);
        //    }
        //    if (res.IsSuccess)
        //    {
        //        TempData["Success"] = "Product added successfully";


        //    }
        //    return RedirectToAction("Index");
        //}

        public async Task<IActionResult> Create(Product prd)
        {
            if (!ModelState.IsValid)
            {
                return View(prd);
            }
            var data = await _services.AddAsync(prd);
            if (!data.IsSuccess)
            {
                ViewBag.ErrorMessage = data.Message;
                return View(prd);
            }
            TempData["Success"] = data.Message;
            return RedirectToAction("Index");
        }

        //public IActionResult Edit(int id)
        //{
        //    var res = _services.GetById(id);
        //    if (!res.IsSuccess)
        //    {
        //        ViewBag.ErrorMessage = res.Message;
        //        return RedirectToAction("Index");
        //    }
        //    return View(res.Data);
        //}

        public async Task<IActionResult>Edit(int Id)
        {
            var data = await _services.GetByIdAsync(Id);
            if (!data.IsSuccess)
            {
                ViewBag.ErrorMessage = data.Message;
                return RedirectToAction("Index");
            }
            return View(data.Data);
        }

        [HttpPost]
        //public IActionResult Edit(int Id,Product prd)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(prd);
        //    }
        //    var res=_services.Update(Id, prd);
        //    if (!res.IsSuccess)
        //    {
        //         ViewBag.ErrorMessage = res.Message;
        //        return View(prd);
        //    }
        //    if (res.IsSuccess)
        //    {
        //        TempData["Success"] = "Product edited successfully";

        //    }
        //    return RedirectToAction("Index");
        //}

        public async Task<IActionResult>Edit(int Id,Product prd)
        {
            if (!ModelState.IsValid)
            {
                return View(prd);
            }
            var res = await _services.UpdateAsync(Id, prd);
            if (!res.IsSuccess)
            {
                ViewBag.ErrorMessage = res.Message;
                return View(prd);
            }

            TempData["Success"] = res.Message;
            return RedirectToAction("Index");
        }
        [HttpPost]
        //public IActionResult Delete (int Id)
        //{
        //    var res = _services.Delete(Id);
        //    TempData["Success"] = "Product deleted successfully";
        //    return RedirectToAction("Index");
        //}

        public async Task<IActionResult>Delete(int Id)
        {
            var res = await _services.DeleteAsync(Id);
            TempData["Success"] = res.Message;
            return RedirectToAction("Index");
        }
    }
}
