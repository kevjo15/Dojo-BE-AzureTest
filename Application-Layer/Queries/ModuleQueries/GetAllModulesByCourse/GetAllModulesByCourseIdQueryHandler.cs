using Domain_Layer.Models.Module;
using Infrastructure_Layer.Repositories.Module;
using MediatR;

namespace Application_Layer.Queries.ModuleQueries.GetAllModulesByCourse
{
    public class GetAllModulesByCourseIdQueryHandler : IRequestHandler<GetAllModulesByCourseIdQuery, List<ModuleModel>>
    {
        private readonly IModuleRepository _moduleRepository;
        public GetAllModulesByCourseIdQueryHandler(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<List<ModuleModel>> Handle(GetAllModulesByCourseIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.CourseId))
                {
                    throw new ArgumentException($"Module with CourseId {request.CourseId} was not found!");
                }

                var listOfModules = await _moduleRepository.GetAllModulesByCourseId(request.CourseId);

                if (!listOfModules.Any())
                {
                    throw new InvalidOperationException($"Course with ID {request.CourseId} was not found!");
                }
                return listOfModules;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching modules: {ex.Message}", ex);
            }
        }
    }
}
