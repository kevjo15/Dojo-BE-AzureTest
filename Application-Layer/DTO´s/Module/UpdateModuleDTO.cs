
namespace Application_Layer.DTO_s.Module
{
    public class UpdateModuleDTO
    {
        public string CourseId { get; set; }
        public string ModulTitle { get; set; }
        public string Description { get; set; }
        public int OrderInCourse { get; set; }
        public string? ResourceURL { get; set; }
    }
}
