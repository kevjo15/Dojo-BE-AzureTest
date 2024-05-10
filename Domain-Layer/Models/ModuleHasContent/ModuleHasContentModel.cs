using Domain_Layer.Models.Content;
using Domain_Layer.Models.Module;

namespace Domain_Layer.Models.ModuleHasContent
{
    public class ModuleHasContentModel
    {
        public string ModuleId { get; set; }
        public ModuleModel Module { get; set; }
        public string ContentId { get; set; }
        public ContentModel Content { get; set; }

    }
}
