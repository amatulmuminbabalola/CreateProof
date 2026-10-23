
using Microsoft.AspNetCore.Mvc;
using CreateProof.Models;

namespace CreateProof.Controllers
{
    public class RevisionsController : Controller
    {
        private static readonly List<Revision> Revisions = new();

        private static readonly string[] RevisionStatuses =
        {
            "Pending",
            "In Progress",
            "Completed"
        };

        public IActionResult Index(int? projectId)
        {
            var revisions = projectId.HasValue
                ? Revisions.Where(r => r.ProjectId == projectId.Value).ToList()
                : Revisions;

            ViewBag.ProjectId = projectId;
            return View(revisions);
        }

        public IActionResult Create(int projectId)
        {
            ViewBag.ProjectId = projectId;
            return View(new Revision { ProjectId = projectId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Revision revision)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProjectId = revision.ProjectId;
                return View(revision);
            }

            revision.Id = Revisions.Count == 0
                ? 1
                : Revisions.Max(r => r.Id) + 1;

            revision.RequestedAt = DateTime.UtcNow;
            Revisions.Add(revision);

            return RedirectToAction(
                nameof(Index),
                new { projectId = revision.ProjectId });
        }

        public IActionResult Edit(int id)
        {
            var revision = Revisions.FirstOrDefault(r => r.Id == id);

            if (revision == null)
                return NotFound();

            return View(revision);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Revision updatedRevision)
        {
            if (id != updatedRevision.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(updatedRevision);

            var revision = Revisions.FirstOrDefault(r => r.Id == id);

            if (revision == null)
                return NotFound();

            revision.Description = updatedRevision.Description;
            revision.Status = updatedRevision.Status;

            if (revision.Status == "Completed")
                revision.CompletedAt = DateTime.UtcNow;
            else
                revision.CompletedAt = null;

            return RedirectToAction(
                nameof(Index),
                new { projectId = revision.ProjectId });
        }
    }
}
