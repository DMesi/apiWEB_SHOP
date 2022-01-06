using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webWS.Models;
using webWS.Repository;

namespace webWS.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ILocationRepository _locRepo;

        public LocationsController(ILocationRepository locRepo)
        {
            _locRepo = locRepo;
        }

        public IActionResult Index()
        {
            return View(new Location() { });
        }

        public async Task<IActionResult> GetAllLocation()
        {
            return Json(new { data = await _locRepo.GetAllAsync(SD.LocationApiPath, HttpContext.Session.GetString("JWToken")) });
        }
    }
}
