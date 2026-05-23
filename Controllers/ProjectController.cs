using kodmeydan.Data;
using kodmeydan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kodmeydan.Controllers
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(projects);
        }
        [HttpGet]
        public IActionResult Create()
       {
          return View();
       }
       [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }
                else
                {
                    var project = new Project
                    {
                        Name = model.Name,
                        Description = model.Description,
                      
                        GitHubUrl = model.GitHubUrl,
                        LiveDemoUrl = model.LiveDemoUrl,
                        Ticket = model.Ticket,
                        Code = model.Code,
                        Content = model.Content,
                        CreatedAt = DateTime.Now,
                        UserId = user.Id
                    };
                    _context.Projects.Add(project);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Proje başarıyla oluşturulmuştur.";
                    return RedirectToAction("Index");
                
                }
                
            }

            return View(model);
        }
        public async Task<ActionResult> Detail(int id)
        {
            var project = await _context.Projects
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            return View(project);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Proje başarıyla silinmiştir.";
            return RedirectToAction("Index");
        }
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var project = await _context.Projects.FindAsync(id);
                if (project == null)
                    return NotFound();

                var model = new ProjectViewModel
                {
                    Name = project.Name,
                    Description = project.Description,
                    GitHubUrl = project.GitHubUrl,
                    LiveDemoUrl = project.LiveDemoUrl,
                    Ticket = project.Ticket,
                    Code = project.Code,
                    Content = project.Content
                };

                return View(model);
            }
            [HttpPost]
            public async Task<IActionResult> Edit(int id, ProjectViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var project = await _context.Projects.FindAsync(id);
                    if (project == null)
                        return NotFound();

                    project.Name = model.Name;
                    project.Description = model.Description;
                    project.GitHubUrl = model.GitHubUrl;
                    project.LiveDemoUrl = model.LiveDemoUrl;
                    project.Ticket = model.Ticket;
                    project.Code = model.Code;
                    project.Content = model.Content;

                    _context.Projects.Update(project);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Proje başarıyla güncellenmiştir.";
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            public async Task<IActionResult> MyProjects()
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var projects = await _context.Projects
                    .Where(p => p.UserId == user.Id)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();

                return View(projects);
            }
    }
}