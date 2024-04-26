using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO_s.Module
{
    public class CreateModuleDTO
    {
        public string CourseId { get; set; } = string.Empty;
        public string ModulTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderInCourse { get; set; }
        public string? ResourceURL { get; set; }
    }
}
