
using CreateProof.Interfaces.Services;
using CreateProof.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreateProof.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAllAsync();
            return View(projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }

            await _projectService.CreateAsync(project);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _projectService.GetByIdAsync(id.Value);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
    }
}
