using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Context;
using SurveySystem.Entities;
using SurveySystem.Models;

namespace SurveySystem.Controllers
{
    public class AdminController : Controller
    {
        // DbContext'i tanımla.
        private readonly SurveyContext _context;
        private readonly UserManager<AppUser> _userManager;
        public AdminController(SurveyContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //Soru ekleme formunu göstermek için bir GET işlemi
        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View();
        }

        // Soru ekleme formunu kullanarak bir POST işlemi
        //[HttpPost]
        //public IActionResult AddQuestion(Question model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(model);
        //        _context.SaveChanges();
        //        return RedirectToAction("QuestionList");
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //[Authorize(Roles = "Admin, Member")] // Admin ve Member rolüne sahip kullanıcılar bu işlemi yapabilir
        //public IActionResult AddQuestion(Question model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Kullanıcı oturum açmış mı kontrol edin
        //        if (User.Identity.IsAuthenticated)
        //        {
        //            // Kullanıcı kimliğini alın
        //            var userId = _userManager.GetUserId(User);

        //            // Kullanıcı admin ise, eklenen soruyu otomatik olarak onayla.
        //            if (User.IsInRole("Admin"))
        //            {
        //                model.IsApproved = true; // Admin eklediği için onaylandı olarak işaretle.
        //            }
        //            else
        //            {
        //                // Üye eklediyse, onay bekleme durumunda bırak.
        //                model.IsApproved = false;
        //            }

        //            // Soru ekleyen kullanıcının kimliğini modeldeki UserId alanına atamamıza gerek yok.
        //            // Kimlik doğrulama işlemleri otomatik olarak kullanıcı kimliğini sağlar.

        //            _context.Add(model);
        //            _context.SaveChanges();
        //            return RedirectToAction("QuestionList");
        //        }
        //        else
        //        {
        //            // Kullanıcı oturum açmamışsa, oturum açma sayfasına yönlendir.
        //            return RedirectToAction("SignIn", "Home");
        //        }
        //    }
        //    return View(model);
        //}

        [HttpPost]
        [Authorize(Roles = "Admin, Member")] // Admin ve Member rolüne sahip kullanıcılar bu işlemi yapabilir
        public IActionResult AddQuestion(Question model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı oturum açmış mı kontrol et.
                if (User.Identity.IsAuthenticated)
                {
                    var userId = _userManager.GetUserId(User);
                    // Kullanıcı admin ise, eklenen soruyu otomatik olarak onayla.
                    if (User.IsInRole("Admin"))
                    {
                        model.IsApproved = true; // Admin eklediği için onaylandı olarak işaretle.
                    }
                    else
                    {
                        // Üye eklediyse, onay bekleme durumunda bırak.
                        model.IsApproved = false;
                    }
                   
                    model.UserId = int.Parse(userId);

                    _context.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("QuestionList");
                }
                else
                {
                    // Kullanıcı oturum açmamışsa, oturum açma sayfasına yönlendir.
                    return RedirectToAction("SignIn", "Home");
                }
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // Hata mesajlarını görüntüle
                    var errorMessage = error.ErrorMessage;
                    // Hangi özellikte hata olduğunu görmek için error.PropertyName'i kullanabilirsiniz.
                }

                // Sorunları düzeltmek için geri dönüş yapmadan önce bu hataları ele alın.
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, Member")] // Soruların bir listesini görüntülemek için kullanılır.
        [HttpGet]
        public IActionResult QuestionList()
        {
            var questions = _context.Questions
                        .OrderByDescending(e => e.Id)
                        .ToList();

            return View(questions);
        }    


        [HttpPost] // Bu eylem, kullanıcının silme işlemini onayladığı zaman çalışır.       
        public IActionResult DeleteQuestion(int id)
        {
            try
            {
                var question = _context.Questions.Find(id);
                if (question == null)
                {
                    return Json(new { isSuccess = false, errorMessage = "Soru bulunamadı." });
                }

                _context.Questions.Remove(question);
                _context.SaveChanges();

                return Json(new { isSuccess = true, message = "Soru başarıyla silindi." });

            }
            catch (Exception ex)
            {
                // Hata durumunu loglayabilir veya inceleyebilirsiniz
                return Json(new { isSuccess = false, errorMessage = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")] // Onay Bekleyen Soruların bir listesini görüntülemek için kullanılır.
        [HttpGet]        
        public IActionResult UnApprovedQuestions()
        {
            var unapprovedQuestions = _context.Questions
                                    .Where(q => !q.IsApproved) // Sadece onaylanmamış soruları seç
                                    .OrderByDescending(q => q.Id)
                                    .ToList();

            return View(unapprovedQuestions);
        }

        [Authorize(Roles = "Admin")] // Sadece Admin rolüne sahip kullanıcılar bu işlemi yapabilir
        [HttpPost]
        public IActionResult ProcessQuestion(int id, bool approve)
        {
            try
            {
                var question = _context.Questions.Find(id);

                if (question == null)
                {
                    return Json(new { success = false, message = "Soru bulunamadı." });
                }

                if (approve)
                {
                    // Soruyu onayla
                    question.IsApproved = true;
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    // Soruyu reddet
                    _context.Questions.Remove(question);
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = "Member")]
        [HttpGet]
        public IActionResult MyQuestionList()
        {
            // Kullanıcının kimliğini al.
            var userId = _userManager.GetUserId(User);

            // Kullanıcının eklediği soruları filtreleyerek al.
            var userQuestions = _context.Questions
                .Where(q => q.UserId == int.Parse(userId))
                .OrderByDescending(q => q.Id)
                .ToList();

            return View(userQuestions);
        }

    }
}
