using Application_Layer.DTO_s.Module;
using MediatR;

namespace Application_Layer.Commands.ModuleCommands.CreateModule
{
    public class CreateModuleCommand : IRequest<CreateModuleResult>
    {
        public CreateModuleDTO ModuleDTO { get; }

        public CreateModuleCommand(CreateModuleDTO moduleDTO)
        {
            ModuleDTO = moduleDTO;
        }
    }
}
