﻿using Microsoft.AspNetCore.Mvc;
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

        // Soruların bir listesini görüntülemek için kullanılır.
        public IActionResult QuestionList()
        {           
            var questions = _context.Questions
                        .OrderByDescending(e => e.Id)
                        .ToList();
                       
            return View(questions);
        }

        //[HttpGet] //Bu eylem, silinecek soruyu bulmak için kullanılır. 
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var question = _context.Questions.Find(id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(question);
        //}


        //[HttpPost, ActionName("Delete")] // Bu eylem, kullanıcının silme işlemini onayladığı zaman çalışır. 
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteQuestion(int id)
        //{
        //    var question = _context.Questions.Find(id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Questions.Remove(question);
        //    _context.SaveChanges();

        //    return RedirectToAction("QuestionList");
        //}



        [HttpPost] // Bu eylem, kullanıcının silme işlemini onayladığı zaman çalışır.       
        public IActionResult DeleteQuestion(int id)
        {
            try
            {
                var question = _context.Questions.Find(id);
                if (question == null)
                {
                    return Json(new { isSuccess = false, errorMessage = "Soru bulunamadı." });
                }

                _context.Questions.Remove(question);
                _context.SaveChanges();

                return Json(new { isSuccess = true, message = "Soru başarıyla silindi." });

            }
            catch (Exception ex)
            {
                // Hata durumunu loglayabilir veya inceleyebilirsiniz
                return Json(new { isSuccess = false, errorMessage = ex.Message });
            }
        }


    }
}