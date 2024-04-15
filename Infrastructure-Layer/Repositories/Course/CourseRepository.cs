using Domain_Layer.Models.CourseModel;
using Infrastructure_Layer.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Repositories.Course
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DojoDBContext _dojoDBContext;

        public CourseRepository(DojoDBContext dojoDBContext)
        {
            _dojoDBContext = dojoDBContext;
        }

        public Task AddCourseAsync(CourseModel course)
        {
            throw new NotImplementedException();
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

        public async Task<List<CourseModel>> GetCoursesBySearchCriteria(string searchCriteria)
        {
            try
            {
                var searchedList = await (from course in _dojoDBContext.CourseModel
                                          join user in _dojoDBContext.User on course.UserId equals user.Id
                                          where course.CourseId.Contains(searchCriteria) || course.Title.Contains(searchCriteria) ||
                                          course.Language.Contains(searchCriteria) ||
                                          (user.FirstName + " " + user.LastName).Contains(searchCriteria)
                                          select course).ToListAsync();

                if (!searchedList.Any())
                {
                    throw new Exception($"There were no courses with the searched criteria: {searchCriteria} in the database");
                }

                return searchedList;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while getting courses with criteria: {searchCriteria} from the database", ex);
            }
        }
    }
}
