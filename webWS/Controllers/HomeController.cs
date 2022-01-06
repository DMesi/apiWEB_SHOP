using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using webWS.Models;
using webWS.Models.ViewModel;
using webWS.Repository;
using webWS.Repository.IRepository;

namespace webWS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountRepository _accRepo;
        private readonly IProductRepository _proRepo;
        private readonly ILocationRepository _locRepo;
        public HomeController(ILogger<HomeController> logger, IAccountRepository accRepo, IProductRepository proRepo, ILocationRepository locRepo)
        {
            _logger = logger;
            _accRepo = accRepo;
            _proRepo = proRepo;
            _locRepo = locRepo;
        }

        public async Task<IActionResult> Index()
        {

            IndexVM listOfProduct = new IndexVM()
            {
                ProductList = await _proRepo.GetAllAsync(SD.ProductsApiPath,""),
                LocationList = await _locRepo.GetAllAsync(SD.LocationApiPath,"")
            };

            return View(listOfProduct);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Login()
        {

            Users obj = new Users();

            return View(obj);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Users obj)
        {

            Users objUser = await _accRepo.LoginAsync(SD.AccountApiPath + "login/", obj);

            if(objUser.Token == null)
            {

                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

           identity.AddClaim(new Claim(ClaimTypes.Name, objUser.email));
            identity.AddClaim(new Claim(ClaimTypes.Role, objUser.Roles));


            //  public ICollection<string> Roles { get; set; }
            //foreach(var uloga in objUser.Roles)
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, uloga));

            //}

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("JWToken", objUser.Token);

            TempData["alert"] = "Welcome" + objUser.email;


            return RedirectToAction("Index");


        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Users obj)
        {

            bool result = await _accRepo.RegisterAsync(SD.AccountApiPath + "register/", obj);

            if (result== false)
            {

                return View();
            }


            TempData["alert"] = "Registration Successful";


            return RedirectToAction("Register");


        }
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");

            return RedirectToAction("Index");


        }
        public IActionResult AccessDenied()
        {


            return View();


        }

    }
}
