using kodmeydan.Data;
using kodmeydan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kodmeydan.Controllers
{
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ArticleController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _context.Articles
                .Include(a => a.User)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return View(articles);
        }
        [HttpGet]
        public IActionResult Create()
       {
          return View();
       }
       [HttpPost]
        public async Task<IActionResult> Create(ArticleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }
                var article = new Article
                {
                    Title = model.Title,
                    Content = model.Content,
                    Ticket = model.Ticket,
                    CreatedAt = DateTime.Now,
                    UserId = user.Id,
                    Link = model.Link
                };
                _context.Articles.Add(article);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Makaleniz başarıyla oluşturulmuştur.";
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public async Task<IActionResult> Detail(int id)
        {      
           var article = await _context.Articles
             .Include(a => a.User)
             .FirstOrDefaultAsync(a => a.Id == id);

           

           if (article == null)
              return NotFound();

            return View(article);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
                return NotFound();

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Makaleniz başarıyla silinmiştir.";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
          var article = await _context.Articles.FindAsync(id);
          if (article == null) return NotFound();

          var model = new ArticleViewModel
          {
             Title = article.Title,
             Content = article.Content,
             Ticket = article.Ticket,
             Link = article.Link
           };

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
          var article = await _context.Articles.FindAsync(id);
          if (article == null) return NotFound();

          article.Title = model.Title;
          article.Content = model.Content;
          article.Ticket = model.Ticket;
          article.Link = model.Link;

          _context.Articles.Update(article);
          await _context.SaveChangesAsync();
          TempData["Success"] = "Makaleniz başarıyla güncellenmiştir.";
          return RedirectToAction("Detail", new { id = article.Id });
        }
        public async Task<IActionResult> MyArticles()
        {
           var user = await _userManager.GetUserAsync(User);
           var articles = await _context.Articles
           .Where(a => a.UserId == user.Id)
           .  OrderByDescending(a => a.CreatedAt)
           .    ToListAsync();

          return View(articles);
        }

    }
} 
       


