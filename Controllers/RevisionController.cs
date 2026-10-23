
using CreateProof.Interfaces.Services;
using CreateProof.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreateProof.Controllers
{
    public class RevisionsController : Controller
    {
        private readonly IRevisionService _revisionService;

        public RevisionsController(IRevisionService revisionService)
        {
            _revisionService = revisionService;
        }

        public async Task<IActionResult> Index(int? projectId)
        {
            var revisions = await _revisionService.GetAllAsync(projectId);

            ViewBag.ProjectId = projectId;
            return View(revisions);
        }

        public IActionResult Create(int projectId)
        {
            ViewBag.ProjectId = projectId;

            return View(new Revision
            {
                ProjectId = projectId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Revision revision)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProjectId = revision.ProjectId;
                return View(revision);
            }

            await _revisionService.CreateAsync(revision);

            return RedirectToAction(
                nameof(Index),
                new { projectId = revision.ProjectId });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var revision = await _revisionService.GetByIdAsync(id);

            if (revision == null)
            {
                return NotFound();
            }

            return View(revision);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, Revision updatedRevision)
        {
            if (id != updatedRevision.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updatedRevision);
            }

            var existingRevision =
                await _revisionService.GetByIdAsync(id);

            if (existingRevision == null)
            {
                return NotFound();
            }

            await _revisionService.UpdateAsync(updatedRevision);

            return RedirectToAction(
                nameof(Index),
                new { projectId = existingRevision.ProjectId });
        }
    }
}
