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

        [HttpPost]
        [Authorize(Roles = "Admin, Member")] // Admin ve Member rolüne sahip kullanıcılar bu işlemi yapabilir
        public IActionResult AddQuestion(Question model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı oturum açmış mı kontrol edin
                if (User.Identity.IsAuthenticated)
                {
                    // Kullanıcı kimliğini alın
                    var userId = _userManager.GetUserId(User);

                    // Kullanıcı admin ise, eklenen soruyu otomatik olarak onaylayın
                    if (User.IsInRole("Admin"))
                    {
                        model.IsApproved = true; // Admin eklediği için onaylandı olarak işaretle
                    }
                    else
                    {
                        // Üye eklediyse, onay bekleme durumunda bırakın.
                        model.IsApproved = false;
                    }

                    // Soru ekleyen kullanıcının kimliğini modeldeki UserId alanına atamamıza gerek yok.
                    // Kimlik doğrulama işlemleri otomatik olarak kullanıcı kimliğini sağlar.

                    _context.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("QuestionList");
                }
                else
                {
                    // Kullanıcı oturum açmamışsa, işlemi reddedin veya oturum açma sayfasına yönlendirin.
                    // Örnek olarak aşağıdaki satırı kullanabilirsiniz:
                    // return RedirectToAction("SignIn", "Home");
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            return View(model);
        }



        // Soruların bir listesini görüntülemek için kullanılır.
        [HttpGet]
        public IActionResult QuestionList()
        {
            var questions = _context.Questions
                        .OrderByDescending(e => e.Id)
                        .ToList();

            return View(questions);
        }

        //[HttpGet] //Bu eylem, silinecek soruyu bulmak için kullanılır. 
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var question = _context.Questions.Find(id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(question);
        //}


        //[HttpPost, ActionName("Delete")] // Bu eylem, kullanıcının silme işlemini onayladığı zaman çalışır. 
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteQuestion(int id)
        //{
        //    var question = _context.Questions.Find(id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Questions.Remove(question);
        //    _context.SaveChanges();

        //    return RedirectToAction("QuestionList");
        //}



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



    }
}
