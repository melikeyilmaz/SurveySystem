using Microsoft.AspNetCore.Mvc;
using SurveySystem.Context;
using SurveySystem.Models;

namespace SurveySystem.Controllers
{
    public class AdminController : Controller
    {
        // DbContext'i tanımla.
        private readonly SurveyContext _context;

        public AdminController(SurveyContext context)
        {
            _context = context;
        }

        //Soru ekleme formunu göstermek için bir GET işlemi
        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View();
        }

        ////Soru ekleme sayfasını gösterir
        //[HttpGet]
        //public IActionResult AddQuestion()
        //{
        //    var model = new Question();

        //    // Örnek olarak, seçenekleri burada oluşturabilirsiniz.
        //    //model.Options = Enumerable.Range(1, 5).Select(i => new Option { OptionText = "Seçenek " + i }).ToList();
        //    //model.Options = new List<Option>();
        //   // model.Options = Enumerable.Range(1, 5).Select(i => new Option()).ToList();
        //    return View(model);
        //}

        // Soru ekleme formunu kullanarak bir POST işlemi
        [HttpPost]
        public IActionResult AddQuestion(Question model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction("QuestionList");
            }
            return View(model);
        }
    

        public IActionResult QuestionList()
        {
            // Soruların bir listesini görüntülemek için kullanılabilir
            var questions = _context.Questions.ToList();
            return View(questions);
        }
    }
}
