
namespace Domain_Layer.Models.ModulModel
{
    public class ModulModel
    {
        public string ModulId { get; set; } = Guid.NewGuid().ToString();
        public string CourseId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderInCourse { get; set; }
        public string? ResourceURL { get; set; }
    }
}
