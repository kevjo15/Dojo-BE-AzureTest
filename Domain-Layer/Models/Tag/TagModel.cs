using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain_Layer.Models.CourseHasTag;

namespace Domain_Layer.Models.Tag
{
    public class TagModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TagId { get; set; } = Guid.NewGuid().ToString();
        public string TagName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<CourseHasTagModel> CourseHasTags { get; set; } = new List<CourseHasTagModel>();
    }
}
