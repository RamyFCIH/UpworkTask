using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UpworkTask.Dtos
{
    public class LeadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CreateLeadDto
    {
        public string Name { get; set; } = string.Empty;
    }
}
