
namespace Application_Layer.DTO_s.Module
{
    public class CreateModuleDTO
    {
        public string ModulTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderInCourse { get; set; }
        public string ResourceURL { get; set; } = string.Empty;
    }
}
