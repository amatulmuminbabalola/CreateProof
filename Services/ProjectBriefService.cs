using CreateProof.Interfaces.Repositories;
using CreateProof.Interfaces.Services;
using CreateProof.Models;
using CreateProof.ViewModels.ProjectBriefs;

namespace CreateProof.Services
{
public class ProjectBriefService : IProjectBriefService
{
private readonly IProjectBriefRepositories _repository;

    public ProjectBriefService(IProjectBriefRepositories repository)
    {
        _repository = repository;
    }

    public async Task<ProjectBriefDetailsViewModel?> GetByIdAsync(int id)
    {
        var brief = await _repository.GetByIdAsync(id);

        return brief == null ? null : MapToDetailsViewModel(brief);
    }

    public async Task<IEnumerable<ProjectBriefDetailsViewModel>>
        GetByProjectIdAsync(int projectId)
    {
        var briefs = await _repository.GetByProjectIdAsync(projectId);

        return briefs.Select(MapToDetailsViewModel);
    }

    public async Task<ProjectBriefDetailsViewModel> CreateAsync(
        CreateProjectBriefViewModel model)
    {
        ValidateBrief(
            model.Description,
            model.Deliverables,
            model.Deadline,
            model.AgreedPrice,
            model.IncludedRevisions,
            model.PaymentTerms);

        var brief = new ProjectBrief
        {
            ProjectId = model.ProjectId,
            Description = model.Description.Trim(),
            Deliverables = model.Deliverables.Trim(),
            Deadline = model.Deadline,
            AgreedPrice = model.AgreedPrice,
            IncludedRevisions = model.IncludedRevisions,
            PaymentTerms = model.PaymentTerms.Trim(),
            ConfirmationStatus = ProjectBriefConfirmationStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(brief);

        return MapToDetailsViewModel(brief);
    }

    public async Task<ProjectBriefDetailsViewModel?> UpdateAsync(
        EditProjectBriefViewModel model)
    {
        var brief = await _repository.GetByIdAsync(model.Id);

        if (brief == null)
        {
            return null;
        }

        if (brief.ConfirmationStatus ==
            ProjectBriefConfirmationStatus.Confirmed)
        {
            throw new InvalidOperationException(
                "A confirmed project brief cannot be edited.");
        }

        ValidateBrief(
            model.Description,
            model.Deliverables,
            model.Deadline,
            model.AgreedPrice,
            model.IncludedRevisions,
            model.PaymentTerms);

        brief.Description = model.Description.Trim();
        brief.Deliverables = model.Deliverables.Trim();
        brief.Deadline = model.Deadline;
        brief.AgreedPrice = model.AgreedPrice;
        brief.IncludedRevisions = model.IncludedRevisions;
        brief.PaymentTerms = model.PaymentTerms.Trim();
        brief.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(brief);

        return MapToDetailsViewModel(brief);
    }

    public async Task<ProjectBriefDetailsViewModel?> ConfirmAsync(
        ConfirmProjectBriefViewModel model,
        string userId)
    {
        var brief = await _repository.GetByIdAsync(model.Id);

        if (brief == null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException(
                "A valid user ID is required to confirm a brief.");
        }

        if (model.Decision == ProjectBriefConfirmationStatus.Pending)
        {
            throw new ArgumentException(
                "A brief cannot be confirmed with a Pending decision.");
        }

        if (!Enum.IsDefined(typeof(ProjectBriefConfirmationStatus),
            model.Decision))
        {
            throw new ArgumentException("Invalid confirmation decision.");
        }

        if (brief.ConfirmationStatus ==
            ProjectBriefConfirmationStatus.Confirmed)
        {
            throw new InvalidOperationException(
                "This project brief has already been confirmed.");
        }

        brief.ConfirmationStatus = model.Decision;
        brief.ConfirmedByUserId =
            model.Decision == ProjectBriefConfirmationStatus.Confirmed
                ? userId
                : null;

        brief.ConfirmedAt =
            model.Decision == ProjectBriefConfirmationStatus.Confirmed
                ? DateTime.UtcNow
                : null;

        brief.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(brief);

        return MapToDetailsViewModel(brief);
    }

    private static void ValidateBrief(
        string description,
        string deliverables,
        DateTime deadline,
        decimal agreedPrice,
        int includedRevisions,
        string paymentTerms)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException(
                "The project description is required.");
        }

        if (string.IsNullOrWhiteSpace(deliverables))
        {
            throw new ArgumentException(
                "The project deliverables are required.");
        }

        if (deadline.Date < DateTime.UtcNow.Date)
        {
            throw new ArgumentException(
                "The deadline cannot be in the past.");
        }

        if (agreedPrice <= 0)
        {
            throw new ArgumentException(
                "The agreed price must be greater than zero.");
        }

        if (includedRevisions < 0 || includedRevisions > 100)
        {
            throw new ArgumentException(
                "Included revisions must be between 0 and 100.");
        }

        if (string.IsNullOrWhiteSpace(paymentTerms))
        {
            throw new ArgumentException(
                "Payment terms are required.");
        }
    }

    private static ProjectBriefDetailsViewModel MapToDetailsViewModel(
        ProjectBrief brief)
    {
        return new ProjectBriefDetailsViewModel
        {
            Id = brief.Id,
            ProjectId = brief.ProjectId,
            Description = brief.Description,
            Deliverables = brief.Deliverables,
            Deadline = brief.Deadline,
            AgreedPrice = brief.AgreedPrice,
            IncludedRevisions = brief.IncludedRevisions,
            PaymentTerms = brief.PaymentTerms,
            ConfirmationStatus = brief.ConfirmationStatus.ToString(),
            CreatedAt = brief.CreatedAt,
            UpdatedAt = brief.UpdatedAt,
            ConfirmedAt = brief.ConfirmedAt,
            ConfirmedByUserId = brief.ConfirmedByUserId
        };
    }
}

}