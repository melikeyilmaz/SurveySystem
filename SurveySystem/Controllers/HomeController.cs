using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Entities;
using SurveySystem.Models;
using System.Diagnostics;

namespace SurveySystem.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
                _userManager = userManager; 
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserCreateModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Email= model.Email,
                    UserName=model.UserName
                };

                var identityResult=await _userManager.CreateAsync(user,model.Password);    
                if (identityResult.Succeeded) 
                { 
                    return RedirectToAction("Index");
                }
                foreach(var error in identityResult.Errors) 
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            return View(model);  
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
    }
}