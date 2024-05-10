using AutoMapper;
using Infrastructure_Layer.Repositories.Module;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application_Layer.Commands.ModuleCommands.UpdateModule
{
    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand, IActionResult>
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;

        public UpdateModuleCommandHandler(IModuleRepository moduleRepository, IMapper mapper)
        {
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var module = await _moduleRepository.GetModuleByIdAsync(request.ModuleId);
                if (module == null)
                {
                    return new NotFoundObjectResult($"Module with ID {request.ModuleId} not found.");
                }

                _mapper.Map(request.ModuleUpdateDTO, module);
                await _moduleRepository.UpdateModuleAsync(module);

                return new OkObjectResult(module);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"An error occurred while updating the module: {ex.Message}");
            }
        }
    }
}
