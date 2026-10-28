using CreateProof.Interfaces.Services;
using CreateProof.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreateProof.Controllers
{
public class ApprovalsController : Controller
{
private readonly IApprovalService _approvalService;

    public ApprovalsController(IApprovalService approvalService)
    {
        _approvalService = approvalService;
    }

    // GET: /Approvals
    public async Task<IActionResult> Index()
    {
        var approvals = await _approvalService.GetAllApprovalsAsync();
        return View(approvals);
    }

    // GET: /Approvals/Details/5
    public async Task<IActionResult> Details(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var approval = await _approvalService.GetApprovalByIdAsync(id);

        if (approval == null)
        {
            return NotFound();
        }

        return View(approval);
    }

    // GET: /Approvals/Create?projectId=5
    [HttpGet]
    public IActionResult Create(int projectId)
    {
        if (projectId <= 0)
        {
            return BadRequest();
        }

        var approval = new Approval
        {
            ProjectId = projectId
        };

        return View(approval);
    }

    // POST: /Approvals/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Approval approval)
    {
        if (!ModelState.IsValid)
        {
            return View(approval);
        }

        var success = await _approvalService
            .SubmitApprovalRequestAsync(approval);

        if (!success)
        {
            ModelState.AddModelError(
                string.Empty,
                "Unable to submit the approval request.");

            return View(approval);
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: /Approvals/Decide/5
    [HttpGet]
    public async Task<IActionResult> Decide(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var approval = await _approvalService.GetApprovalByIdAsync(id);

        if (approval == null)
        {
            return NotFound();
        }

        if (approval.Decision != ApprovalDecision.pending)
        {
            return BadRequest("This approval request has already been decided.");
        }

        return View(approval);
    }

    // POST: /Approvals/Decide
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Decide(
        int id,
        ApprovalDecision decision,
        string? clientFeedback)
    {
        if (decision == ApprovalDecision.pending ||
            !Enum.IsDefined(typeof(ApprovalDecision), decision))
        {
            ModelState.AddModelError(
                nameof(decision),
                "Please select a valid client decision.");

            var existingApproval =
                await _approvalService.GetApprovalByIdAsync(id);

            if (existingApproval == null)
            {
                return NotFound();
            }

            return View(existingApproval);
        }

        var success = await _approvalService.RecordClientDecisionAsync(
            id,
            decision,
            clientFeedback);

        if (!success)
        {
            ModelState.AddModelError(
                string.Empty,
                "Unable to record the decision. The request may already have been decided.");

            var approval = await _approvalService.GetApprovalByIdAsync(id);

            if (approval == null)
            {
                return NotFound();
            }

            return View(approval);
        }

        return RedirectToAction(nameof(Details), new { id });
    }
}

}