using CreateProof.Models;

namespace CreateProof.Interfaces.Services
{
public interface IApprovalService
{
Task<IEnumerable<Approval>> GetAllApprovalsAsync();

    Task<Approval?> GetApprovalByIdAsync(int id);

    Task<IEnumerable<Approval>> GetApprovalsByProjectIdAsync(int projectId);

    Task<bool> SubmitApprovalRequestAsync(Approval approval);

    Task<bool> RecordClientDecisionAsync(
        int approvalId,
        ApprovalDecision decision,
        string? clientFeedback);
}

}