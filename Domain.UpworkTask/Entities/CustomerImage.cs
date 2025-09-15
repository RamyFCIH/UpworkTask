using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UpworkTask.Entities
{
    public class CustomerImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Base64Image { get; set; } = string.Empty;

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }

}
