using System.ComponentModel.DataAnnotations;
namespace CreateProof.Models
{
    public class Approval
    {
        public int Id {get; set;}
        [Required]
        public int ProjectId {get; set;}
        [Required]
        public ApprovalDecision Decision {get; set;}
        = ApprovalDecision.pending;
        [StringLength(2000)]
        public string? ClientFeedback {get; set;}
        public DateTime SubmittedAt {get; set;}
        public DateTime? DecisionAt {get; set;}
    }
    public enum ApprovalDecision
    {
        pending = 0,
        Approved = 1,
        Rejected = 2,
        ChangesRequested = 3
    }
}