using Domain_Layer.CommandOperationResult;
using Domain_Layer.Models.Course;

namespace Infrastructure_Layer.Repositories.Course
{
    public interface ICourseRepository
    {
        Task DeleteCourseByIdAsync(string courseId);
        Task AddCourseAsync(CourseModel course);
        Task<CourseModel> GetCourseByIdAsync(string courseId);
        Task<List<CourseModel>> GetCoursesBySearchCriteria(SearchCriteria searchCriteriaInfo);
        Task<bool> UpdateCourseAsync(CourseModel courseToUpdate);
        Task<List<CourseModel>> GetAllCourses();
        Task DeleteCourseHasModuleConnection(string courseId, string moduleId);
        Task<OperationResult<bool>> ConnectCourseWithModuleAsync(string courseId, string moduleId);
    }
}
