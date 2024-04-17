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
        public async Task AddCourseAsync(CourseModel course)
        {
            _dojoDBContext.CourseModel.Add(course);
            await _dojoDBContext.SaveChangesAsync();
        }
        public async Task<List<CourseModel>> GetCoursesBySearchCriteria(SearchCriteria searchCriteria)
        {
            try
            {
                // Trim and check if search criteria are not empty or whitespace
                var courseId = searchCriteria.CourseId?.Trim();
                var title = searchCriteria.Title?.Trim();
                var categoryOrSubject = searchCriteria.CategoryOrSubject?.Trim();
                var language = searchCriteria.Language?.Trim();
                var firstName = searchCriteria.FirstName?.Trim();
                var lastName = searchCriteria.LastName?.Trim();

                // Combine first and last name for the user search
                var fullName = $"{firstName} {lastName}".Trim();

                //if there is no search criteria we are returning all courses
                if (searchCriteria.SearchBySearchTerm == false)
                {
                    return _dojoDBContext.CourseModel.ToList();
                }


                var searchedList = await (from course in _dojoDBContext.CourseModel
                                          join user in _dojoDBContext.User on course.UserId equals user.Id
                                          where (course.CourseId.Equals(courseId) || course.Title.Equals(title) ||
                                                 course.CategoryOrSubject.Equals(categoryOrSubject) || course.Language.Equals(language) ||
                                                (user.FirstName + " " + user.LastName).Equals(fullName))
                                          select course).ToListAsync();
                if (!searchedList.Any())
                {

                    throw new InvalidOperationException("No courses found matching the search term.");
                }

                return searchedList;


            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new Exception($"An error occurred while fetching courses: {ex.Message}", ex);
            }
        }
    }
}
