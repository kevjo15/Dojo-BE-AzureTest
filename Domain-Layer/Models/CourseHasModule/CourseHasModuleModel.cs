using Domain_Layer.Models.Course;
using Domain_Layer.Models.Module;

namespace Domain_Layer.Models.CourseHasModule
{
    public class CourseHasModuleModel
    {
        public string CourseId { get; set; }
        public CourseModel Course { get; set; }
        public string ModuleId { get; set; }
        public ModuleModel Module { get; set; }

    }
}
