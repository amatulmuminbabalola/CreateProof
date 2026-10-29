using System.ComponentModel.DataAnnotations;
using CreateProof.Models;

namespace CreateProof.ViewModels.ProjectBriefs
{
    public class ConfirmProjectBriefViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public ProjectBriefConfirmationStatus Decision { get; set; }

        [StringLength(2000)]
        public string? Comments { get; set; }
    }
}