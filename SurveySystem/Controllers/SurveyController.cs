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
        public IActionResult NonMemberSurvey(Survey model)
        {
            if (ModelState.IsValid)
            {
                // Yeni bir anket oluşturun
                var survey = new Survey
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Questions = new List<Question>()
                };

                // Her bir soruyu döngü içinde işleyin ve anket nesnesine ekleyin
                foreach (var question in model.Questions)
                {
                    var existingQuestion = _context.Questions.FirstOrDefault(q => q.Id == question.Id);
                    if (existingQuestion != null)
                    {
                        // Radio butonlardan gelen seçimi doğru cevap olarak ayarlayın
                        int correctChoiceId = Convert.ToInt32(Request.Form[$"Questions[{model.Questions.IndexOf(question)}].CorrectChoiceId"]);

                        // Checkbox'un adından seçilen sorunun ID'sini alın
                        int selectedQuestionId = Convert.ToInt32(Request.Form[$"Questions[{model.Questions.IndexOf(question)}].SelectedQuestionId"]);

                        // Doğru cevabı kaydedin
                        var correctAnswer = new CorrectAnswer
                        {
                            Question = existingQuestion,
                            CorrectChoiceId = correctChoiceId,
                            QuestionId = selectedQuestionId
                        };

                        // CorrectAnswer'ı veritabanına ekleyin
                        _context.CorrectAnswers.Add(correctAnswer);

                        survey.Questions.Add(existingQuestion);
                    }
                }

                // Anketi veritabanına ekleyin
                _context.Surveys.Add(survey);
                _context.SaveChanges();

                // Başarılı bir sonuç döndürün veya yönlendirin
                return RedirectToAction("ThankYou");
            }

            // Model geçerli değilse, hata mesajları ile birlikte sayfayı tekrar göster
            return View(model);
        }





    }
}
