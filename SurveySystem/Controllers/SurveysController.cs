using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Context;
using SurveySystem.Migrations;
using SurveySystem.Models;

namespace SurveySystem.Controllers
{
    public class SurveysController : Controller
    {
        private readonly SurveyContext _context;

        public SurveysController(SurveyContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> NonMemberSurvey()
        //{
        //    var questions = await _context.Questions.ToListAsync();

        //    var survey = new Surveys
        //    {
        //        Questions = questions
        //    };

        //    return View(survey);
        //}

        public IActionResult NonMemberSurvey()
        {
            var questions = _context.Questions.ToList();

            return View(questions);
        }

        [HttpPost]
        public IActionResult NonMemberSurvey(Surveys survey)
        {
            if (ModelState.IsValid)
            {
                // Ankete ilişkin verileri işleme veya kaydetme işlemleri burada yapılabilir
                // survey.UserName ile kullanıcının adına erişebilirsiniz
                // survey.SelectedQuestions ile seçilen sorulara ve doğru cevaplara erişebilirsiniz

                // Örneğin, anketi kaydetme işlemi için bir servis veya veritabanı kullanabilirsiniz.
                // Örneğin: surveyService.CreateSurvey(survey);

                // Başarıyla oluşturulduktan sonra başka bir sayfaya yönlendirin veya teşekkür mesajı gösterin
                return RedirectToAction("ThankYou");
            }

            // Geçersiz giriş varsa tekrar formu göster
            survey.Questions = _context.Questions.ToList();
            return View(survey);
        }
    }
}
