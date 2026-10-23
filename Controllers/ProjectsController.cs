
using Microsoft.AspNetCore.Mvc;
using CreateProof.Models;

namespace CreateProof.Controllers
{
    public class ProjectsController : Controller
    {
        // Temporary in-memory storage while MySQL is unavailable.
        private static readonly List<Project> Projects = new();

        public IActionResult Index()
        {
            return View(Projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }

            project.Id = Projects.Count == 0
                ? 1
                : Projects.Max(p => p.Id) + 1;

            Projects.Add(project);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = Projects.FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
    }
}
