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


        public IActionResult SurveyDetails(int id)
        {

            // Verilen anket ID'sine göre ilgili anketi veritabanından çekin
            var survey = _context.Surveys.Include(s => s.SurveyResponses)
                                            .ThenInclude(sr => sr.SurveyScore)
                                            .SingleOrDefault(s => s.Id == id);

            if (survey == null)
            {
                return NotFound();
            }

            // Anket katılımcılarını gruplayarak al.
            var participants = survey.SurveyResponses.GroupBy(sr => sr.SurveyScoreId)
                                                    .Select(group => group.First().SurveyScore)
                                                    .ToList();

            return View(participants);

        }

    }
}
