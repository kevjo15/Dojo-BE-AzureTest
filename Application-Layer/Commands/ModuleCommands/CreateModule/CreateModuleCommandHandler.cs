using AutoMapper;
using Domain_Layer.Models.Module;
using Infrastructure_Layer.Repositories.Module;
using MediatR;

namespace Application_Layer.Commands.ModuleCommands.CreateModule
{
    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, CreateModuleResult>
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IMapper _mapper;

        public CreateModuleCommandHandler(IModuleRepository moduleRepository, IMapper mapper)
        {
            _moduleRepository = moduleRepository;
            _mapper = mapper;
        }

        public async Task<CreateModuleResult> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var modulModel = _mapper.Map<ModuleModel>(request.ModuleDTO);
                await _moduleRepository.CreateModuleAsync(modulModel);
                return new CreateModuleResult { Success = true, Message = "Module successfully created" };
            }
            catch (Exception ex)
            {
                return new CreateModuleResult { Success = false, Message = "An error occurred: " + ex.Message };
            }
        }
    }
}
