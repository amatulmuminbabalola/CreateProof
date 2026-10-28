using System.ComponentModel.DataAnnotations;
namespace CreateProof.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]

        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]

        public string ClientName {  get; set; } = string.Empty;
        [Required]
        [StringLength(50)]

        public string CreativeWorkType {  get; set; } = string.Empty;
        [StringLength(1000)]

        public string? Description {  get; set; }
        [Range (0, 1000000000)]

        public decimal AgreedPrice { get; set; }
        public DateTime? Deadline { get; set; }
        [Required]
        public string Status { get; set; } = "Pending";
    }
}
