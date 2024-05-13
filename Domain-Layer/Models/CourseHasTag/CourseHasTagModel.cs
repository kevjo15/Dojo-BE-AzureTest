using Domain_Layer.Models.Course;
using Domain_Layer.Models.Tag;

namespace Domain_Layer.Models.CourseHasTag
{
    public class CourseHasTagModel
    {
        public string CourseId { get; set; }
        public CourseModel? Course { get; set; }
        public string TagId { get; set; }
        public TagModel? Tag { get; set; }
    }
}
