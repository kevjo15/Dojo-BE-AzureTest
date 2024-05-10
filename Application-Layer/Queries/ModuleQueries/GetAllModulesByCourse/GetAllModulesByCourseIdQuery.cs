using Domain_Layer.Models.Module;
using MediatR;

namespace Application_Layer.Queries.ModuleQueries.GetAllModulesByCourse
{
    public class GetAllModulesByCourseIdQuery : IRequest<List<ModuleModel>>
    {
        public string CourseId;
        public GetAllModulesByCourseIdQuery(string courseId)
        {
            CourseId = courseId;
        }
    }
}
