

using Domain_Layer.Models.CourseModel;

namespace Infrastructure_Layer.Repositories.Course
{
    public interface ICourseRepository
    {
        Task DeleteCourseByIdAsync(string courseId);
        Task<bool> UpdateCourseAsync(CourseModel courseToUpdate);
    }
}
