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

                // Benzersiz bir kimlik oluştur
                string uniqueId = Guid.NewGuid().ToString();

                // Survey verisini oluşturun
                var survey = new Survey
                {
                    FirstName = surveyData.FirstName,
                    LastName = surveyData.LastName,
                    SubmissionDate = DateTime.Now,
                    QuestionResponses = surveyData.QuestionResponses,
                    UniqueId = uniqueId // Benzersiz kimliği ankete atayın
                };

                // Survey verisini veritabanına ekleyin ve kaydedin
                _context.Surveys.Add(survey);
                _context.SaveChanges();

                // Oluşturulan benzersiz URL'yi oluşturun
                string surveyUrl = Url.Action("NonMemberSurvey", "Survey", new { uniqueId });

                return Json(new { success = true, surveyUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult AnsweringSurvey(string uniqueId)
        {

            var surveyId = 7; // Anketin ID'sini belirleyin veya dilediğiniz bir şekilde alın

            var surveyWithQuestions = _context.Surveys
                 .Where(survey => survey.UniqueId == uniqueId)
                .Include(survey => survey.QuestionResponses) // İlgili soruların cevaplarını çekmek için Include kullanın
                .ThenInclude(response => response.Question) // Soruları da çekmek için Include kullanın
                .FirstOrDefault(survey => survey.Id == surveyId);

            if (surveyWithQuestions != null)
            {
                var questions = surveyWithQuestions.QuestionResponses
                    .Select(response => new Question
                    {
                        QuestionText = response.Question.QuestionText,
                        Option1 = response.Question.Option1,
                        Option2 = response.Question.Option2,
                        Option3 = response.Question.Option3,
                        Option4 = response.Question.Option4,
                        Option5 = response.Question.Option5,
                        //SelectedOption = response.SelectedOption
                    })
                    .ToList();
                surveyWithQuestions.Questions = questions;
                // questions listesi şimdi ilgili ankete ait soruları ve cevaplarını içeriyor
            }
            else
            {
                // Belirtilen anket ID'si ile eşleşen anket bulunamadı
            }
           
            return View(surveyWithQuestions);
        }

        //public async Task<IActionResult> AnsweringSurvey(int surveyId)
        //{
        //    // Veritabanından ankete ait tüm soruları çekin
        //    var answeringSurvey = await _context.Questions
        //        .Where(q => q.Surveys.Any(s => s.Id == surveyId))
        //        .ToListAsync();

        //    return View(answeringSurvey);
        //}
        [HttpGet]
        public IActionResult SurveyLink()
        {

            return View();
        }

        //public IActionResult AnsweringSurvey(string uniqueId)
        //{
        //    // Benzersiz ID'ye sahip anketi veritabanından alın
        //    var survey = _context.Surveys.SingleOrDefault(s => s.UniqueId == uniqueId);

        //    if (survey == null)
        //    {
        //        // Anket bulunamadı, hata sayfasına yönlendirin veya uygun bir işlem yapın
        //        return RedirectToAction("Error");
        //    }

        //    // Anketi görüntüleme sayfasını göster
        //    return View(survey);
        //}


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

        //        // Benzersiz bir kimlik oluştur
        //        string uniqueId = Guid.NewGuid().ToString();

        //        // Survey verisini oluşturun
        //        var survey = new Survey
        //        {
        //            FirstName = surveyData.FirstName,
        //            LastName = surveyData.LastName,
        //            SubmissionDate = DateTime.Now,
        //            QuestionResponses = surveyData.QuestionResponses,
        //            UniqueId = uniqueId // Benzersiz kimliği ankete atayın
        //        };

        //        // Survey verisini veritabanına ekleyin ve kaydedin
        //        _context.Surveys.Add(survey);
        //        _context.SaveChanges();

        //        // Oluşturulan benzersiz URL'yi oluşturun
        //        string surveyUrl = Url.Action("SurveyPage", "ControllerName", new { uniqueId });

        //        return Json(new { success = true, surveyUrl });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}



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

        //        // Survey verisini oluşturun
        //        var survey = new Survey
        //        {
        //            FirstName = surveyData.FirstName,
        //            LastName = surveyData.LastName,
        //            SubmissionDate = DateTime.Now,
        //            QuestionResponses = surveyData.QuestionResponses // Birden fazla soru ve cevapları buraya ekleyin
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
