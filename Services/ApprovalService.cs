using CreateProof.Interfaces;
using CreateProof.Models;
using CreateProof.Interfaces.Services;
using CreateProof.Interfaces.Repositories;

namespace CreateProof.Services
{
public class ApprovalService : IApprovalService
{
private readonly IApprovalRepository _approvalRepository;

    public ApprovalService(IApprovalRepository approvalRepository)
    {
        _approvalRepository = approvalRepository;
    }

    public async Task<IEnumerable<Approval>> GetAllApprovalsAsync()
    {
        return await _approvalRepository.GetAllAsync();
    }

    public async Task<Approval?> GetApprovalByIdAsync(int id)
    {
        if (id <= 0)
        {
            return null;
        }

        return await _approvalRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Approval>> GetApprovalsByProjectIdAsync(int projectId)
    {
        if (projectId <= 0)
        {
            return Enumerable.Empty<Approval>();
        }

        return await _approvalRepository.GetByProjectIdAsync(projectId);
    }

    public async Task<bool> SubmitApprovalRequestAsync(Approval approval)
    {
        if (approval == null || approval.ProjectId <= 0)
        {
            return false;
        }

        approval.Id = 0;
        approval.Decision = ApprovalDecision.pending;
        approval.ClientFeedback = null;
        approval.SubmittedAt = DateTime.UtcNow;
        approval.DecisionAt = null;

        await _approvalRepository.AddAsync(approval);

        return true;
    }

    public async Task<bool> RecordClientDecisionAsync(
        int approvalId,
        ApprovalDecision decision,
        string? ClientFeedback)
    {
        if (approvalId <= 0)
        {
            return false;
        }

        if (decision == ApprovalDecision.pending ||
            !Enum.IsDefined(typeof(ApprovalDecision), decision))
        {
            return false;
        }

        var approval = await _approvalRepository.GetByIdAsync(approvalId);

        if (approval == null)
        {
            return false;
        }

        if (approval.Decision != ApprovalDecision.pending)
        {
            return false;
        }

        approval.Decision = decision;
        approval.ClientFeedback = string.IsNullOrWhiteSpace(ClientFeedback)
            ? null
            : ClientFeedback.Trim();

        approval.DecisionAt = DateTime.UtcNow;

        await _approvalRepository.UpdateAsync(approval);

        return true;
    }
}

}