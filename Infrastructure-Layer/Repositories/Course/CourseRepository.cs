using Domain_Layer.CommandOperationResult;
using Domain_Layer.Models.Course;
using Domain_Layer.Models.CourseHasModule;
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
            if (course == null)
            {
                throw new Exception($"Course with courseId: {courseId} is not existing in the database");
            }
            _dojoDBContext.CourseModel.Remove(course);
            await _dojoDBContext.SaveChangesAsync();
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
                return wantedCourse;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while getting a course with Id {courseId} from the database", ex);
            }
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
            var courseId = searchCriteria.CourseId;
            var title = searchCriteria.Title;
            var categoryOrSubject = searchCriteria.CategoryOrSubject;
            var language = searchCriteria.Language;
            var firstName = searchCriteria.FirstName;
            var lastName = searchCriteria.LastName;

            // Combine first and last name for the user search
            var fullName = $"{firstName} {lastName}";

            var searchedList = await (from course in _dojoDBContext.CourseModel
                                      join user in _dojoDBContext.User on course.UserId equals user.Id
                                      where (course.CourseId.Equals(courseId) || course.Title.Equals(title) ||
                                             course.CategoryOrSubject.Equals(categoryOrSubject) || course.Language.Equals(language) ||
                                            (user.FirstName + " " + user.LastName).Equals(fullName))
                                      select course).ToListAsync();

            return searchedList;
        }

        public Task<List<CourseModel>> GetAllCourses()
        {
            return _dojoDBContext.CourseModel.ToListAsync();
        }
        public async Task DeleteCourseHasModuleConnection(string courseId, string moduleId)
        {
            var connection = await _dojoDBContext.CourseHasModules.FindAsync(courseId, moduleId);
            if (connection == null)
            {
                throw new Exception($"There was no connection between courseId: {courseId} and moduleId: {moduleId} in the database");
            }
            _dojoDBContext.CourseHasModules.Remove(connection);
            await _dojoDBContext.SaveChangesAsync();
        }

        public async Task<OperationResult<bool>> ConnectCourseWithModuleAsync(string courseId, string moduleId)
        {
            // Retrieve the existing course and module
            var course = await _dojoDBContext.CourseModel.FindAsync(courseId);
            var module = await _dojoDBContext.ModuleModel.FindAsync(moduleId);

            // Return false if either entity is not found
            if (course == null || module == null)
            {
                return new OperationResult<bool> { Success = false, Message = "Course or module not found." };
            }

            // Check if the association already exists
            var existingAssociation = await _dojoDBContext.CourseHasModules
             .FirstOrDefaultAsync(chm => chm.Course.CourseId == courseId && chm.Module.ModuleId == moduleId);

            if (existingAssociation != null)
            {
                return new OperationResult<bool> { Success = false, Message = $"Association between course ID {courseId} and module ID {moduleId} already exists." };
            }

            // Create a new association
            var courseHasModule = new CourseHasModuleModel
            {
                Course = course,
                Module = module
            };

            // Add the association to the course's modules
            course.CourseHasModules.Add(courseHasModule);

            // Save changes
            await _dojoDBContext.SaveChangesAsync();

            return new OperationResult<bool> { Success = true, Data = true };
        }
    }
}
