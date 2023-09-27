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


        //[HttpPost]
        //public IActionResult SaveSurvey(Survey surveyData)
        //{
        //    try
        //    {
        //        // Verileri doğrulama
        //        if (!ModelState.IsValid)
        //        {
        //            // Geçersiz veri varsa hata mesajlarını döndür
        //            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        //            return Json(new { success = false, errors });
        //        }


        //        // Survey verisini oluşturun
        //        var survey = new Survey
        //        {
        //            FirstName = surveyData.FirstName,
        //            LastName = surveyData.LastName,
        //            SubmissionDate = DateTime.Now, // Gönderilme tarihini şu anki tarih olarak ayarla
        //            QuestionId = surveyData.QuestionId,
        //            SelectedOption = surveyData.SelectedOption
        //        };

        //        // Survey verisini veritabanına ekleyin ve kaydedin
        //        _context.Surveys.Add(survey);
        //        _context.SaveChanges();


        //        // Başarılı yanıt gönder
        //        return Json(new { success = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Hata oluştuysa hata mesajını döndür
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public IActionResult SaveSurvey(Survey surveyData)
        //{
        //    ModelState.Clear(); // ModelState'i temizleyin
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Yeni bir anket oluşturun
        //            var survey = new Survey
        //            {
        //                FirstName = surveyData.FirstName,
        //                LastName = surveyData.LastName,
        //                Questions = new List<Question>()
        //            };

        //            // Her bir soruyu döngü içinde işleyin ve anket nesnesine ekleyin
        //            foreach (var question in surveyData.Questions)
        //            {
        //                var existingQuestion = _context.Questions.FirstOrDefault(q => q.Id == question.Id);
        //                var existingCorrectAnswer = _context.CorrectAnswers.FirstOrDefault(ca => ca.QuestionId == question.Id);

        //                if (existingQuestion != null && existingCorrectAnswer != null)
        //                {
        //                    // Doğru cevabı oluşturun
        //                    var correctAnswer = new CorrectAnswer
        //                    {
        //                        SurveyId = survey.Id,
        //                        QuestionId = existingQuestion.Id,
        //                        CorrectChoiceId = existingCorrectAnswer.CorrectChoiceId
        //                    };

        //                    // CorrectAnswer'ı veritabanına ekleyin
        //                    _context.CorrectAnswers.Add(correctAnswer);

        //                    survey.Questions.Add(existingQuestion);
        //                }
        //            }

        //            // Anketi veritabanına ekleyin
        //            _context.Surveys.Add(survey);
        //            _context.SaveChanges();

        //            return Json(new { success = true });
        //        }
        //        catch (Exception ex)
        //        {
        //            // İç istisna ayrıntılarını alın
        //            var innerException = ex.InnerException;
        //            while (innerException != null)
        //            {
        //                // InnerException'ın mesajını veya başka ayrıntılarını loglayın veya inceleyin
        //                // Loglama yapabilir veya hata mesajını innerException.Message ile alabilirsiniz.
        //                innerException = innerException.InnerException;
        //            }

        //            return Json(new { success = false, message = "An error occurred while saving the entity changes. See the inner exception for details." });
        //        }
        //    }

        //    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
        //    return Json(new { success = false, message = errors });
        //}

        //[HttpPost]
        //public IActionResult SaveSurvey(Survey model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Yeni bir anket oluşturun
        //        var survey = new Survey
        //        {
        //            FirstName = model.FirstName,
        //            LastName = model.LastName,
        //            Questions = new List<Question>()
        //        };

        //        // Her bir soruyu döngü içinde işleyin ve anket nesnesine ekleyin
        //        foreach (var question in model.Questions)
        //        {
        //            var existingQuestion = _context.Questions.FirstOrDefault(q => q.Id == question.Id);
        //            if (existingQuestion != null)
        //            {
        //                survey.Questions.Add(existingQuestion);
        //            }
        //        }

        //        // Anketi veritabanına ekleyin
        //        _context.Surveys.Add(survey);
        //        _context.SaveChanges();

        //        // Başarılı bir sonuç döndürün veya yönlendirin
        //        return RedirectToAction("ThankYou");
        //    }

        //    // Model geçerli değilse, hata mesajları ile birlikte sayfayı tekrar göster
        //    return View(model);
        //}




    }
}
