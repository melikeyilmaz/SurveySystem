using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Context;
using SurveySystem.Models;
using X.PagedList;

namespace SurveySystem.Controllers
{
    public class ScoreController : Controller
    {
        private readonly SurveyContext _context;
        public ScoreController(SurveyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            int pageSize = 10; // Her sayfada gösterilecek soru sayısını belirleyin.

            IPagedList<Survey> surveys = _context.Surveys.ToPagedList(page, pageSize);

            return View(surveys);

        }

        //public IActionResult SurveyDetails(int id)
        //{
        //    // İlgili anketi veritabanından çekin
        //    var survey = _context.Surveys
        //        .Include(s => s.SurveyResponses)
        //        .ThenInclude(sr => sr.Question)
        //        .SingleOrDefault(s => s.Id == id);

        //    if (survey == null)
        //    {
        //        return NotFound(); // Anket bulunamazsa 404 hatası döndürün
        //    }

        //    var surveyResponses = survey.SurveyResponses.ToList();

        //    var correctResponses = 0;
        //    var incorrectResponses = 0;

        //    foreach (var surveyResponse in surveyResponses)
        //    {
        //        var question = _context.QuestionResponse.FirstOrDefault(qr =>
        //            qr.QuestionId == surveyResponse.QuestionId && qr.SurveyId == surveyResponse.SurveyId);

        //        if (question != null && surveyResponse.SelectedOption == question.SelectedOption)
        //        {
        //            correctResponses++;
        //        }
        //        else
        //        {
        //            incorrectResponses++;
        //        }
        //    }

        //    var surveyScore = new SurveyScore // Yeni bir SurveyScore nesnesi oluşturun
        //    {
        //        FirstName = survey.FirstName,
        //        LastName = survey.LastName,
        //        Score = correctResponses, // Doğru cevap sayısını kullanın
        //        SurveyResponses = surveyResponses
        //    };

        //    return View(surveyScore);
        //}


        //public IActionResult SurveyDetails(int id)
        //{
        //    // İlgili anketi veritabanından çekin
        //    var survey = _context.Surveys
        //        .Include(s => s.SurveyResponses)
        //        .ThenInclude(sr => sr.Question)
        //        .SingleOrDefault(s => s.Id == id);

        //    if (survey == null)
        //    {
        //        return NotFound(); // Anket bulunamazsa 404 hatası döndürün
        //    }

        //    var surveyResponses = survey.SurveyResponses.ToList();

        //    var correctResponses = 0;
        //    var incorrectResponses = 0;

        //    foreach (var surveyResponse in surveyResponses)
        //    {
        //        var question = _context.QuestionResponse.FirstOrDefault(qr =>
        //            qr.QuestionId == surveyResponse.QuestionId && qr.SurveyId == surveyResponse.SurveyId);

        //        if (question != null && surveyResponse.SelectedOption == question.SelectedOption)
        //        {
        //            correctResponses++;
        //        }
        //        else
        //        {
        //            incorrectResponses++;
        //        }
        //    }

        //    var surveyScore = new SurveyScore // Yeni bir SurveyScore nesnesi oluşturun
        //    {
        //        FirstName = survey.FirstName,
        //        LastName = survey.LastName,
        //        Score = correctResponses, // Doğru cevap sayısını kullanın
        //        SurveyResponses = surveyResponses
        //    };

        //    // SurveyScore modelini liste içine alın
        //    var surveyScores = new List<SurveyScore> { surveyScore };

        //    return View(surveyScores); // Modeli liste içine alarak view'a gönderin
        //}


        private (int CorrectResponses, int IncorrectResponses) CalculateResponses(List<SurveyResponse> surveyResponses)
        {
            var correctResponses = 0;
            var incorrectResponses = 0;

            foreach (var surveyResponse in surveyResponses)
            {
                var questionResponse = _context.QuestionResponse.FirstOrDefault(qr =>
                    qr.QuestionId == surveyResponse.QuestionId && qr.SurveyId == surveyResponse.SurveyId);

                if (questionResponse != null && surveyResponse.SelectedOption == questionResponse.SelectedOption)
                {
                    correctResponses++;
                }
                else
                {
                    incorrectResponses++;
                }
            }

            return (correctResponses, incorrectResponses);
        }

        public IActionResult SurveyDetails(int id)
        {
            // İlgili anketi veritabanından çekin
            var survey = _context.Surveys
                .Include(s => s.SurveyResponses)
                .ThenInclude(sr => sr.Question)
                .SingleOrDefault(s => s.Id == id);

            if (survey == null)
            {
                return NotFound(); // Anket bulunamazsa 404 hatası döndürün
            }

            var surveyResponses = survey.SurveyResponses.ToList();

            // CalculateResponses fonksiyonunu kullanarak doğru ve yanlış cevapları hesaplayın
            var (correctResponses, incorrectResponses) = CalculateResponses(surveyResponses);

            var surveyScores = surveyResponses
                            .Select(sr => _context.SurveyScores.FirstOrDefault(ss => ss.Id == sr.SurveyScoreId))
                            .Where(ss => ss != null)
                            .Distinct()
                            .ToList();

            // Doğru ve yanlış cevap sayılarını view'a taşıyın
            ViewBag.CorrectResponses = correctResponses;
            ViewBag.IncorrectResponses = incorrectResponses;

            return View(surveyScores);
        }


        //public IActionResult SurveyDetails(int id)
        //{
        //    // İlgili anketi veritabanından çekin
        //    var survey = _context.Surveys
        //        .Include(s => s.SurveyResponses)
        //        .ThenInclude(sr => sr.Question)
        //        .SingleOrDefault(s => s.Id == id);

        //    if (survey == null)
        //    {
        //        return NotFound(); // Anket bulunamazsa 404 hatası döndürün
        //    }

        //    var surveyResponses = survey.SurveyResponses.ToList();

        //    var correctResponses = 0;
        //    var incorrectResponses = 0;

        //    foreach (var surveyResponse in surveyResponses)
        //    {
        //        var questionResponse = _context.QuestionResponse.FirstOrDefault(qr =>
        //            qr.QuestionId == surveyResponse.QuestionId && qr.SurveyId == surveyResponse.SurveyId);

        //        if (questionResponse != null && surveyResponse.SelectedOption == questionResponse.SelectedOption)
        //        {
        //            correctResponses++;
        //        }
        //        else
        //        {
        //            incorrectResponses++;
        //        }
        //    }

        //    var surveyScores = surveyResponses
        //                    .Select(sr => _context.SurveyScores.FirstOrDefault(ss => ss.Id == sr.SurveyScoreId))
        //                    .Where(ss => ss != null)
        //                    .Distinct()
        //                    .ToList();

        //    // Doğru ve yanlış cevap sayılarını view'a taşıyın
        //    ViewBag.CorrectResponses = correctResponses;
        //    ViewBag.IncorrectResponses = incorrectResponses;

        //    return View(surveyScores);

        //    //var surveyScore = new SurveyScore // Yeni bir SurveyScore nesnesi oluşturun
        //    //{
        //    //    FirstName = survey.FirstName,
        //    //    LastName = survey.LastName,
        //    //    Score = correctResponses, // Doğru cevap sayısını kullanın
        //    //    SurveyResponses = surveyResponses
        //    //};

        //    //// SurveyScore modelini liste içine alın
        //    //var surveyScores = new List<SurveyScore> { surveyScore };

        //    //return View(surveyScores); // Modeli liste içine alarak view'a gönderin
        //}






    }
}
