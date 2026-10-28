using System;
using System.ComponentModel.DataAnnotations;

namespace CreateProof.ViewModels.ProjectBriefs
{
    public class CreateProjectBriefViewModel
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(3000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Deliverables { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [Required]
        [Range(0.01, 999999999)]
        public decimal AgreedPrice { get; set; }

        [Required]
        [Range(0, 100)]
        public int IncludedRevisions { get; set; }

        [Required]
        [StringLength(2000)]
        public string PaymentTerms { get; set; } = string.Empty;
    }
}