
using System.ComponentModel.DataAnnotations;

namespace Domain.UpworkTask.Entities
{
    public class LeadImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Base64Image { get; set; } = string.Empty;

        [Required]
        public int LeadId { get; set; }

        public Lead Lead { get; set; }
    }
}
