using System;

namespace CreateProof.ViewModels.ProjectBriefs
{
    public class ProjectBriefDetailsViewModel
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Deliverables { get; set; } = string.Empty;

        public DateTime Deadline { get; set; }

        public decimal AgreedPrice { get; set; }

        public int IncludedRevisions { get; set; }

        public string PaymentTerms { get; set; } = string.Empty;

        public string ConfirmationStatus { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? ConfirmedAt { get; set; }

        public string? ConfirmedByUserId { get; set; }
    }
}