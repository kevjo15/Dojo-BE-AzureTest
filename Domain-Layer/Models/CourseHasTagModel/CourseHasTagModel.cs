
namespace Domain_Layer.Models.CourseHasTagModel
{
    public class CourseHasTagModel
    {
        public string CourseTagId { get; set; } = Guid.NewGuid().ToString();
        public required string CourseId { get; set; }
        public required string TagId { get; set; }
    }
}
