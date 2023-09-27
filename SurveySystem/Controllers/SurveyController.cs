using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Context;
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

        //[HttpGet]
        //public IActionResult NonMemberSurvey()
        //{
        //    //var questions = _context.Questions.ToList();

        //    //return View(questions);

        //    var survey = new Survey
        //    {
        //        Questions = _context.Questions.ToList()
        //    };

        //    return View(survey);
        //}

        [HttpGet]
        public IActionResult NonMemberSurvey()
        {
            var random = new Random();
            var allQuestions = _context.Questions.ToList();

            var selectedQuestions = new List<Question>();

            // Eğer soru sayısı 10'dan azsa, tüm soruları seçiyoruz.
            if (allQuestions.Count <= 10)
            {
                selectedQuestions = allQuestions;
            }
            else
            {
                // Rastgele 10 soru seçiyoruz.
                while (selectedQuestions.Count < 10)
                {
                    var randomIndex = random.Next(0, allQuestions.Count);
                    var randomQuestion = allQuestions[randomIndex];

                    // Aynı soruyu birden fazla kez seçmemeye dikkat ediyoruz.
                    if (!selectedQuestions.Contains(randomQuestion))
                    {
                        selectedQuestions.Add(randomQuestion);
                    }
                }
            }

            var survey = new Survey
            {
                Questions = selectedQuestions
            };

            return View(survey);
        }


        public IActionResult SaveSurvey(Survey surveyData)
        {
            try
            {
                // Verileri doğrulama
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, errors });
                }

                // Survey verisini oluşturun
                var survey = new Survey
                {
                    FirstName = surveyData.FirstName,
                    LastName = surveyData.LastName,
                    SubmissionDate = DateTime.Now,
                    QuestionResponses = surveyData.QuestionResponses // Birden fazla soru ve cevapları buraya ekleyin
                };

                // Survey verisini veritabanına ekleyin ve kaydedin
                _context.Surveys.Add(survey);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }




        //public IActionResult SaveSurvey(Survey surveyData)
        //{
        //    try
        //    {
        //        // Verileri doğrulama
        //        if (!ModelState.IsValid)
        //        {
        //            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        //            return Json(new { success = false, errors });
        //        }

        //        // Sistemde tanımlı olan 10 soruyu alın
        //        var allQuestions = _context.Questions.ToList();

        //        // Kullanıcının seçtiği soruları kontrol et
        //        if (surveyData.QuestionResponses == null || surveyData.QuestionResponses.Count != 4)
        //        {
        //            return Json(new { success = false, message = "Üye olmayan kullanıcılar 4 soruluk bir anket oluşturabilir!" });
        //        }

        //        foreach (var selectedQuestion in surveyData.QuestionResponses)
        //        {
        //            if (!allQuestions.Any(q => q.Id == selectedQuestion.QuestionId))
        //            {
        //                return Json(new { success = false, message = "Geçersiz soru seçildi." });
        //            }
        //        }

        //        // Survey verisini oluşturun
        //        var survey = new Survey
        //        {
        //            FirstName = surveyData.FirstName,
        //            LastName = surveyData.LastName,
        //            SubmissionDate = DateTime.Now,
        //            QuestionResponses = surveyData.QuestionResponses
        //        };

        //        // Survey verisini veritabanına ekleyin ve kaydedin
        //        _context.Surveys.Add(survey);
        //        _context.SaveChanges();

        //        return Json(new { success = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}

    }
}
