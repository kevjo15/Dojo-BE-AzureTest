
namespace Domain_Layer.Models.ContentModel
{
    public class ContentModel
    {
        public string ContentId { get; set; } = Guid.NewGuid().ToString();
        public string ModulId { get; set; } = string.Empty;
        public string ContentTitle { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string? ContentURL { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
