using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain_Layer.Models.CourseHasModule;
using Domain_Layer.Models.ModuleHasContent;

namespace Domain_Layer.Models.Module
{
    public class ModuleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ModuleId { get; set; } = Guid.NewGuid().ToString();
        public string ModuleTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderInCourse { get; set; }
        public string? ResourceURL { get; set; }
        public ICollection<ModuleHasContentModel> ModuleHasContents { get; set; } = new List<ModuleHasContentModel>();
        public ICollection<CourseHasModuleModel> CourseHasModules { get; set; } = new List<CourseHasModuleModel>();
    }
}
