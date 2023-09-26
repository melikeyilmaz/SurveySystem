using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Context;
using SurveySystem.Migrations;
using SurveySystem.Models;

namespace SurveySystem.Controllers
{
    public class SurveyController : Controller
    {
        private readonly SurveyContext _context;

        public SurveyController(SurveyContext context)
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
        [HttpGet]
        public IActionResult NonMemberSurvey()
        {
            //var questions = _context.Questions.ToList();

            //return View(questions);

            var survey = new Survey
            {
                Questions = _context.Questions.ToList()
            };

            return View(survey);
        }

        [HttpPost]
        public IActionResult NonMemberSurvey(Survey survey)
        {
            if (ModelState.IsValid)
            {
                // Veriyi veritabanına ekleyin
                _context.Surveys.Add(survey);

                // Değişiklikleri kaydedin
                _context.SaveChanges();

                // İşlem başarılıysa başka bir sayfaya yönlendirin
                return RedirectToAction("Success");
            }

            // Eğer ModelState geçersizse, formu tekrar gösterin ve hataları gösterin
            return View(survey);
        }


    }
}
