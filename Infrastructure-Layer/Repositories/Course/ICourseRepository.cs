

using Domain_Layer.Models.CourseModel;

namespace Infrastructure_Layer.Repositories.Course
{
    public interface ICourseRepository
    {
        Task DeleteCourseByIdAsync(string courseId);
        Task<CourseModel> GetCourseByIdAsync(string courseId);
    }
}
