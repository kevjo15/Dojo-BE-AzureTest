using Infrastructure_Layer.Repositories.Module;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application_Layer.Queries.ModuleQueries.GetModuleById
{
    public class GetModuleByIdQueryHandler : IRequestHandler<GetModuleByIdQuery, IActionResult>
    {
        private readonly IModuleRepository _moduleRepository;

        public GetModuleByIdQueryHandler(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<IActionResult> Handle(GetModuleByIdQuery request, CancellationToken cancellationToken)
        {
            var module = await _moduleRepository.GetModuleByIdAsync(request.ModuleId);
            if (module == null)
            {
                return new NotFoundObjectResult($"No module found with ID {request.ModuleId}");
            }

            return new OkObjectResult(module);
        }
    }
}
