using kodmeydan.Data;
using kodmeydan.Models;
using Microsoft.AspNetCore.Mvc;

namespace kodmeydan.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(int? articleId, int? projectId)
        {
            if (articleId == null && projectId == null)
                return BadRequest();

            var model = new Report
            {
                ArticleId = articleId,
                ProjectId = projectId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Report model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.CreatedAt = DateTime.Now;
            _context.Reports.Add(model);
            await _context.SaveChangesAsync();


            TempData["Success"] = "Raporunuz başarıyla iletilmiştir.";


            return RedirectToAction("Index", "Home");
        }
    }
}