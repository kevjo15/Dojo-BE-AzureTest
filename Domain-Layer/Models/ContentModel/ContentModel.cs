
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain_Layer.Models.ContentModel
{
    public class ContentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ContentId { get; set; } = Guid.NewGuid().ToString();
        public string ModulId { get; set; } = string.Empty;
        public string ContentTitle { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string? ContentURL { get; set; }
        public string Description { get; set; } = string.Empty;
        public ICollection<Domain_Layer.Models.ModulModel.ModulModel> Modules { get; set; }
    }
}
