using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain_Layer.Models.ModuleHasContent;

namespace Domain_Layer.Models.Content
{
    public class ContentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ContentId { get; set; } = Guid.NewGuid().ToString();
        public string ContentTitle { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ContentURL { get; set; }
        public ICollection<ModuleHasContentModel> ModuleHasContents { get; set; } = new List<ModuleHasContentModel>();
    }
}
