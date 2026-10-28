using System.Security.Claims;
using CreateProof.Interfaces.Services;
using CreateProof.ViewModels.ProjectBriefs;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreateProof.Controllers
{
// [Authorize]
public class ProjectBriefsController : Controller
{
private readonly IProjectBriefService _projectBriefService;

    public ProjectBriefsController(
        IProjectBriefService projectBriefService)
    {
        _projectBriefService = projectBriefService;
    }

    // GET: /ProjectBriefs/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var brief = await _projectBriefService.GetByIdAsync(id);

        if (brief == null)
        {
            return NotFound();
        }

        return View(brief);
    }

    // GET: /ProjectBriefs/ByProject/5
    [HttpGet]
    public async Task<IActionResult> ByProject(int projectId)
    {
        if (projectId <= 0)
        {
            return BadRequest();
        }

        var briefs = await _projectBriefService
            .GetByProjectIdAsync(projectId);

        return View(briefs);
    }

    // GET: /ProjectBriefs/Create?projectId=5
    [HttpGet]
    public IActionResult Create(int projectId)
    {
        if (projectId <= 0)
        {
            return BadRequest();
        }

        var model = new CreateProjectBriefViewModel
        {
            ProjectId = projectId,
            Deadline = DateTime.Today.AddDays(7)
        };

        return View(model);
    }

    // POST: /ProjectBriefs/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateProjectBriefViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var brief = await _projectBriefService.CreateAsync(model);

            return RedirectToAction(
                nameof(Details),
                new { id = brief.Id });
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    // GET: /ProjectBriefs/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var brief = await _projectBriefService.GetByIdAsync(id);

        if (brief == null)
        {
            return NotFound();
        }

        var model = new EditProjectBriefViewModel
        {
            Id = brief.Id,
            Description = brief.Description,
            Deliverables = brief.Deliverables,
            Deadline = brief.Deadline,
            AgreedPrice = brief.AgreedPrice,
            IncludedRevisions = brief.IncludedRevisions,
            PaymentTerms = brief.PaymentTerms
        };

        return View(model);
    }

    // POST: /ProjectBriefs/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditProjectBriefViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var brief = await _projectBriefService.UpdateAsync(model);

            if (brief == null)
            {
                return NotFound();
            }

            return RedirectToAction(
                nameof(Details),
                new { id = brief.Id });
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    // GET: /ProjectBriefs/Confirm/5
    [HttpGet]
    public async Task<IActionResult> Confirm(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var brief = await _projectBriefService.GetByIdAsync(id);

        if (brief == null)
        {
            return NotFound();
        }

        var model = new ConfirmProjectBriefViewModel
        {
            Id = brief.Id
        };

        return View(model);
    }

    // POST: /ProjectBriefs/Confirm
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirm(
        ConfirmProjectBriefViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
        {
            return Forbid();
        }

        try
        {
            var brief = await _projectBriefService.ConfirmAsync(
                model,
                userId);

            if (brief == null)
            {
                return NotFound();
            }

            return RedirectToAction(
                nameof(Details),
                new { id = brief.Id });
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }
}

}