using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Context;
using SurveySystem.Entities;
using SurveySystem.Models;
using System.Drawing.Printing;
using X.PagedList;

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
                        model.ApprovalStatus = ApprovalStatus.Approved; // Admin eklediği için onaylandı olarak işaretle.
                    }
                    else
                    {
                        // Üye eklediyse, onay bekleme durumunda bırak.
                        model.ApprovalStatus = ApprovalStatus.PendingApproval;
                    }
                   
                    model.UserId = int.Parse(userId);

                    _context.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("AddQuestion","Admin");
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


        [Authorize(Roles = "Admin,Member")] // Soruların bir listesini görüntülemek için kullanılır.
        [HttpGet]
        public IActionResult QuestionList(int page=1)
        {           
            
            int pageSize = 10; // Her sayfada gösterilecek soru sayısını belirleyin.

            IPagedList<Question> approvedQuestions = _context.Questions
                .Where(q => q.ApprovalStatus == ApprovalStatus.Approved) // Sadece Onaylanmış soruları listele.
                .OrderByDescending(q => q.Id)
                .ToPagedList(page, pageSize); // Sayfa numarası ve sayfa boyutunu belirleyin.

            return View(approvedQuestions);

            //var approvedQuestions = _context.Questions
            //             .Where(q => q.ApprovalStatus == ApprovalStatus.Approved) // Sadece Onaylanmış soruları listele.
            //             .OrderByDescending(q => q.Id)
            //             .ToList();

            //return View(approvedQuestions);
        }    


        [HttpPost] // Bu eylem, admin silme işlemini onayladığı zaman çalışır.       
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
        public IActionResult UnApprovedQuestions(int page = 1)
        {
            int pageSize = 10;

            IPagedList<Question> unapprovedQuestions = _context.Questions
                        .Where(q => q.ApprovalStatus == ApprovalStatus.PendingApproval) // Onay bekleyen soruları seç
                        .OrderByDescending(q => q.Id)
                        .ToPagedList(page, pageSize);

            return View(unapprovedQuestions);

            //var unapprovedQuestions = _context.Questions
            //            .Where(q => q.ApprovalStatus == ApprovalStatus.PendingApproval) // Onay bekleyen soruları seç
            //            .OrderByDescending(q => q.Id)
            //            .ToList();

            //return View(unapprovedQuestions);
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
                    question.ApprovalStatus = ApprovalStatus.Approved; // Onaylandı durumunu ayarla
                    _context.SaveChanges();
                    return Json(new { success = true, status = "Onaylandı" });
                }
                else
                {
                    // Soruyu reddet
                    question.ApprovalStatus = ApprovalStatus.Rejected; // Reddedildi durumunu ayarla
                    _context.SaveChanges();
                    return Json(new { success = true, status = "Reddedildi" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [Authorize(Roles = "Member")]
        [HttpGet]
        public IActionResult MyQuestionList(int page = 1)
        {
            int pageSize = 10;
            // Kullanıcının kimliğini al.
            var userId = _userManager.GetUserId(User);

            // Kullanıcının eklediği soruları filtreleyerek al.
            IPagedList<Question> userQuestions = _context.Questions
                .Where(q => q.UserId == int.Parse(userId))
                .OrderByDescending(q => q.Id)
                .ToPagedList(page, pageSize);

            return View(userQuestions);

            //// Kullanıcının kimliğini al.
            //var userId = _userManager.GetUserId(User);

            //// Kullanıcının eklediği soruları filtreleyerek al.
            //var userQuestions = _context.Questions
            //    .Where(q => q.UserId == int.Parse(userId))
            //    .OrderByDescending(q => q.Id)
            //    .ToList();

            //return View(userQuestions);
        }

    }
}
