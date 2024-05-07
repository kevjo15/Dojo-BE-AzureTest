
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain_Layer.Models.ModulModel
{
    public class ModulModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ModulId { get; set; } = Guid.NewGuid().ToString();
        public string CourseId { get; set; } = string.Empty;
        public string ModulTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderInCourse { get; set; }
        public string? ResourceURL { get; set; }
        public ICollection<Domain_Layer.Models.ContentModel.ContentModel> Contents { get; set; }
    }
}
