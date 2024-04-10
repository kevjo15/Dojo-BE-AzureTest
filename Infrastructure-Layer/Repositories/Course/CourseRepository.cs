using Domain_Layer.Models.CourseModel;
using Infrastructure_Layer.Database;

namespace Infrastructure_Layer.Repositories.Course
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DojoDBContext _dojoDBContext;

        public CourseRepository(DojoDBContext dojoDBContext)
        {
            _dojoDBContext = dojoDBContext;
        }

        public async Task DeleteCourseByIdAsync(string courseId)
        {
            var course = await _dojoDBContext.CourseModel.FindAsync(courseId);
            if (course != null)
            {
                _dojoDBContext.CourseModel.Remove(course);
                await _dojoDBContext.SaveChangesAsync();
            }
        }

        public async Task<CourseModel> GetCourseByIdAsync(string courseId)
        {
            try
            {
                CourseModel? wantedCourse = await _dojoDBContext.CourseModel.FindAsync(courseId);

                if (wantedCourse == null)
                {
                    throw new Exception($"There was no course with Id {courseId} in the database");
                }

                return await Task.FromResult(wantedCourse);
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occured while getting a course with Id {courseId} from database", ex);
            }
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateCourseAsync(CourseModel courseToUpdate)
        {
            _dojoDBContext.CourseModel.Update(courseToUpdate);
            await _dojoDBContext.SaveChangesAsync();

            return true;
        }

    }
}
