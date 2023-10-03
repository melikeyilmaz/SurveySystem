using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Context;
using SurveySystem.Entities;
using SurveySystem.Models;
using System.Security.Claims;

namespace SurveySystem.Controllers
{
    public class SurveyController : Controller
    {
        private readonly SurveyContext _context;
        private readonly UserManager<AppUser> _userManager;
        public SurveyController(SurveyContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult NonMemberSurvey()
        {
            var random = new Random();
            // Sadece Onaylanan soruları seçmek için filtreleme yapın
            var allQuestions = _context.Questions
                                .Where(q => q.ApprovalStatus == ApprovalStatus.Approved)
                                .ToList();

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

        [HttpPost]
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
                    SurveyTitle = surveyData.SurveyTitle,
                    QuestionResponses = surveyData.QuestionResponses,
                    UniqueId = uniqueId, // Benzersiz kimliği ankete atayın
                    IsMember = surveyData.IsMember
                };

                // Survey verisini veritabanına ekleyin ve kaydedin
                _context.Surveys.Add(survey);
                _context.SaveChanges();

                // Oluşturulan benzersiz URL'yi oluşturun
                string surveyUrl = Url.Action("NonMemberSurvey", "Survey", new { uniqueId });

                return Json(new { success = true, surveyUrl, uniqueId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        [HttpGet]
        public IActionResult AnsweringSurvey(string uniqueId, int surveyId)
        {
            try
            {
                // URL'nin hatalı girilip girilmediğini kontrol et
                if (string.IsNullOrEmpty(uniqueId))
                {
                    return Json(new { error = "Geçerli bir URL giriniz." });
                }

                // Veritabanından aynı "SurveyId" ile ilişkilendirilmiş farklı "SurveyScoreId" değerlerini sayın
                int uniqueSurveyScoreCount = _context.SurveyResponses
                         .Where(sr => sr.Survey.UniqueId == uniqueId && !sr.Survey.IsMember) // UniqueId'ye ve IsMember özelliğine göre filtrele
                         .Select(sr => sr.SurveyScoreId)
                         .Distinct()
                         .Count();

                //if (!User.Identity.IsAuthenticated)
                //{
                // Üye olmayan kullanıcı işlemleri
                if (uniqueSurveyScoreCount >= 5)
                {
                    // Kontenjan doluysa özel bir JSON yanıtı döndürün
                    return Json(new { error = "Anketin cevaplanma kontenjanı dolmuştur. 5 kişi ile sınırlıdır." });
                }
                //}

                var surveyWithQuestions = _context.Surveys
                    .Where(survey => survey.UniqueId == uniqueId)
                    .Include(survey => survey.QuestionResponses)
                    .ThenInclude(response => response.Question)
                    .FirstOrDefault();

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
                        })
                        .ToList();

                    surveyWithQuestions.Questions = questions;
                }

                return View(surveyWithQuestions);
            }
            catch (Exception ex)
            {
                // Sunucu tarafında bir hata oluştuğunda bu blok çalışır
                // Hata yakalandığında özel bir hata mesajı oluşturabilirsiniz
                return BadRequest("Sunucu hatası: " + ex.Message);
            }
        }


        [HttpGet]
        public IActionResult SurveyLink()
        {
            // Verileri veritabanından çekin veya başka bir kaynaktan alın
            var survey = _context.Surveys.FirstOrDefault(); // Örnek bir sorgu

            if (survey != null)
            {
                // Verileri modele ekleyin ve view'a gönderin
                return View(survey);
            }
            else
            {
                // Anket verisi yoksa, ViewBag veya ViewData kullanarak uyarı mesajını ayarlayın
                ViewBag.SurveyMessage = "Henüz hiç anket oluşturulmamıştır.";
                return View(); // Uyarı mesajını içeren görünümü görüntüle
            }
        }


        [HttpGet]
        public IActionResult GetSurveyByUniqueId(string uniqueId)
        {
            try
            {
                // uniqueId ile anketi veritabanından bulun
                var survey = _context.Surveys.FirstOrDefault(s => s.UniqueId == uniqueId);

                if (survey != null)
                {
                    // Anket bulundu, JSON formatında geri döndürün
                    return Json(survey);
                }
                else
                {
                    // Belirtilen uniqueId ile eşleşen anket bulunamadı
                    return Json(new { error = "Belirtilen Unique ID ile eşleşen anket bulunamadı." });
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını JSON formatında geri döndürün
                return Json(new { error = "Anketi çekerken bir hata oluştu." });
            }
        }


        [HttpPost]
        public IActionResult CompleteSurveyAnswer(SurveyScore surveyScoreData)
        {
            try
            {
                // Verileri doğrulama
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return Json(new { success = false, errors });
                }

                // Doğru ve yanlış cevapları tutacak değişkenleri tanımlayın
                int correctAnswers = 0;
                int incorrectAnswers = 0;

                // Survey verisini oluşturun
                var surveyanswer = new SurveyScore
                {
                    FirstName = surveyScoreData.FirstName,
                    LastName = surveyScoreData.LastName,
                    SurveyResponses = surveyScoreData.SurveyResponses,
                    //RelatedSurveyId=surveyScoreData.RelatedSurveyId,
                    Score = surveyScoreData.Score,
                };

                // Her bir SurveyResponse için doğru ve yanlış cevapları sayın
                foreach (var response in surveyScoreData.SurveyResponses)
                {
                    var question = _context.QuestionResponse.FirstOrDefault(qr =>
                        qr.QuestionId == response.QuestionId && qr.SurveyId == response.SurveyId);
                    if (question != null && response.SelectedOption == question.SelectedOption)
                    {
                        // Doğru cevap
                        correctAnswers++;
                    }
                    else
                    {
                        // Yanlış cevap
                        incorrectAnswers++;
                    }

                }

                // Doğru ve yanlış cevapları SurveyScore nesnesine kaydedin
                surveyanswer.CorrectAnswers = correctAnswers;
                surveyanswer.IncorrectAnswers = incorrectAnswers;

                // Survey verisini veritabanına ekleyin ve kaydedin
                _context.Add(surveyanswer);
                _context.SaveChanges();

                // Eklendikten sonra surveyScoreId'yi alın
                int surveyScoreId = surveyanswer.Id;

                SurveyScore(surveyScoreId); // SurveyScore işlemini çağır
                                            // SurveyScore işlemini çağırmadan önce yönlendirme yapın
                                            //return RedirectToAction("SurveyScore", new { surveyScoreId = surveyScoreId });
                return Json(new { success = true, message = "Anket başarıyla cevaplandı.", surveyScoreId = surveyScoreId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        /* surveyResponses üzerinde bir döngü oluşturulur ve her bir anket cevabı için aşağıdaki işlemler yapılır:

        a.question adlı bir değişken oluşturulur ve _context.QuestionResponse.FirstOrDefault metodu kullanılarak belirli bir anket cevabına 
         karşılık gelen soruyu bulmaya çalışır. Bu, anket cevabının hangi soruya ait olduğunu ve seçilen seçeneğin hangi soru için doğru
         olduğunu belirlemeye yardımcı olur.Eğer belirtilen koşullara sahip bir soru bulunamazsa, question null olacaktır.

        b.Eğer question null değilse ve anket cevabının seçilen seçeneği ile sorunun seçilen seçeneği aynı ise, bu cevap doğru kabul edilir
         ve correctResponses değişkeni artırılır.Aksi halde, cevap yanlış kabul edilir ve incorrectResponses değişkeni artırılır. */

        public IActionResult SurveyScore(int surveyScoreId)
        {
            //var surveyScoreId =8;

            var surveyResponses = _context.SurveyResponses.Where(sr => sr.SurveyScoreId == surveyScoreId).ToList();

            var correctResponses = 0;
            var incorrectResponses = 0;

            foreach (var surveyResponse in surveyResponses)
            {
                var question = _context.QuestionResponse.FirstOrDefault(qr =>
                    qr.QuestionId == surveyResponse.QuestionId && qr.SurveyId == surveyResponse.SurveyId);

                if (question != null && surveyResponse.SelectedOption == question.SelectedOption)
                {
                    correctResponses++;
                }
                else
                {
                    incorrectResponses++;
                }
            }

            var surveyScore = _context.SurveyScores.FirstOrDefault(ss => ss.Id == surveyScoreId);

            // Eğer surveyScore null ise 0 olarak varsayılan bir değer kullanabilirsiniz.
            ViewBag.SurveyScore = surveyScore?.Score ?? 0;
            ViewBag.CorrectResponses = correctResponses;
            ViewBag.IncorrectResponses = incorrectResponses;

            // return RedirectToAction("SignIn", "Home");
            return View();
        }


        /* Üye olan kullanıcılar 10 soruluk bir anket oluşturabilir. Bu anketi oluştururken sistemde tanımlı tüm
        sorular üzerinden seçim yaparak oluşturabilir.*/
        [Authorize(Roles = "Member")]
        [HttpGet]
        public IActionResult MemberSurvey()
        {
            if (User.Identity.IsAuthenticated)
            {
                var loggedInUserId = _userManager.GetUserId(User);
                var user = _userManager.FindByIdAsync(loggedInUserId).Result;

                if (user != null)
                {
                    var firstName = user.FirstName;
                    var lastName = user.LastName;

                    // Kullanıcı adı ve soyadını ViewBag ile görünüme gönder.
                    ViewBag.FirstName = firstName;
                    ViewBag.LastName = lastName;
                }
            }

            // Sadece Onaylanan soruları seçmek için filtreleme yapın
            var allQuestions = _context.Questions
                               .Where(q => q.ApprovalStatus == ApprovalStatus.Approved)
                               .ToList();

            var survey = new Survey
            {
                Questions = allQuestions
            };

            return View(survey);
        }
   
    }
}
