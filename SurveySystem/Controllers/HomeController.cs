﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Entities;
using SurveySystem.Models;
using System.Diagnostics;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SurveySystem.Controllers
{
    [AutoValidateAntiforgeryToken] //CSRF (Cross-Site Request Forgery) saldırılarına karşı koruma
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            var model = new UserCreateModel();
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(UserCreateModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AppUser user = new()
        //        {
        //            Email = model.Email,
        //            UserName = model.UserName
        //        };

        //        var identityResult = await _userManager.CreateAsync(user, model.Password);
        //        if (identityResult.Succeeded)
        //        {
        //            var memberRole = await _roleManager.FindByNameAsync("Member");
        //            if (memberRole == null)
        //            {
        //                //Kullanıcı kayıt işlemi sırasında rol ataması.
        //                await _roleManager.CreateAsync(new()
        //                {
        //                    Name = "Member",
        //                    CreatedTime = DateTime.Now,
        //                });
        //            }

        //            await _userManager.AddToRoleAsync(user, "Member");
        //            return RedirectToAction("Index");
        //        }
        //        foreach (var error in identityResult.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //    }
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = model.Email,
                    // Kullanıcı adını e-posta ile doldurun, ancak boş veya null ise e-posta adresini kullanılır.
                    UserName = string.IsNullOrWhiteSpace(model.UserName) ? model.Email : model.UserName,
                    //UserName = model.Email, // Kullanıcı adı olarak e-posta kullanabilirsiniz veya farklı bir değer atayabilirsiniz.
                    FirstName = model.FirstName, // Ad alanını ayarlayın
                    LastName = model.LastName,   // Soyad alanını ayarlayın
                };

                var identityResult = await _userManager.CreateAsync(user, model.Password);
                if (identityResult.Succeeded)
                {
                    var memberRole = await _roleManager.FindByNameAsync("Member");
                    if (memberRole == null)
                    {
                        // Kullanıcı kayıt işlemi sırasında rol ataması.
                        await _roleManager.CreateAsync(new()
                        {
                            Name = "Member",
                            CreatedTime = DateTime.Now,
                        });
                    }

                    await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Index");
                }

                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult SignIn(string returnUrl)
        {
            //var model = new UserSignInModel { ReturnUrl = returnUrl };
            var model = new UserSignInModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                //Bir SignIn işleminin sonucu bir tane olabilir.
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                

                if (signInResult.Succeeded)
                {
                    //if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    //{
                    //    return Redirect(model.ReturnUrl);
                    //}
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("AddQuestion", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("AddQuestion", "Admin");
                    }
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");

            }
            return View(model);
        }

        //[Authorize(Roles ="Admin")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var userName = User.Identity.Name;
            var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            User.IsInRole("Member");

            return View();
        }


        [Authorize(Roles = "Admin")] //Sadece Admin'in görebileceği ekran
        public IActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Roles = "Member")]
        public IActionResult Panel()
        {
            return View();
        }

        [Authorize(Roles = "Member")]
        public IActionResult MemberPage()
        {
            return View();
        }
                
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
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