using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using webWS.Models;
using webWS.Repository;
using webWS.Repository.IRepository;

namespace webWS.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ILocationRepository _locRepo;

        public ProductsController(IProductRepository productRepo, ILocationRepository locRepo)
        {
            _productRepo = productRepo;
            _locRepo = locRepo;
        }
        public IActionResult Index()
        {
            return View(new Product() { });
        }

        // update i insert
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Upsert(int? id)
        {
            Product obj = new Product();
            if (id == null)
            {
                //insert / create
                return View(obj);
            }

            // update
            obj = await _productRepo.GetAsync(SD.ProductsApiPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if (obj == null)
            {

                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Product obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using(var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }

                    obj.Picture = p1;
                }
                else
                {
                    var objFromDb = await _productRepo.GetAsync(SD.ProductsApiPath, obj.Id, HttpContext.Session.GetString("JWToken"));
                    obj.Picture = objFromDb.Picture;
                }

                if(obj.Id == 0)
                {

                    await _productRepo.CreateAsync(SD.ProductsApiPath, obj, HttpContext.Session.GetString("JWToken"));

                }
                else
                {
                    await _productRepo.UpdateAsync(SD.ProductsApiPath+obj.Id, obj, HttpContext.Session.GetString("JWToken"));

                }

                return RedirectToAction(nameof(Index));

            }
            else
            {

                return View(obj);
            }

        }



            public async Task<IActionResult> GetAllProducts()
        {
            return Json(new { data = await _productRepo.GetAllAsync(SD.ProductsApiPath,HttpContext.Session.GetString("JWToken")) });
        }

        public async Task<IActionResult> GetAllProductslByCategory(int category)
        {
            return Json(new { data = await _productRepo.GetAllByCategoryAsync(SD.ProductsByCategoryApiPath, category) });
        }

  
        public async Task<IActionResult> CategoryMeni()
        {
            return Json(new { data = await _productRepo.GetAllAsync(SD.ProductsApiPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _productRepo.DeleteAsync(SD.ProductsApiPath, id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {

                return Json(new {success =true, message="Delete Successful!"});
            }
            return Json(new { success = false, message = "Delete Not Successful!" });
        }
           
        
    }
}
