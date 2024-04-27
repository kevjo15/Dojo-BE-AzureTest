using Domain_Layer.Models.ModulModel;
using MediatR;

namespace Application_Layer.Queries.ModuleQueries.GetAllModulesByCourse
{
    public class GetAllModulesByCourseIdQuery : IRequest<List<ModulModel>>
    {
        public string CourseId;
        public GetAllModulesByCourseIdQuery(string courseId)
        {
            CourseId = courseId;
        }
    }
}
