using MediatR;

namespace Application_Layer.Commands.ModuleCommands.DeleteModule
{
    public class DeleteModuleCommand : IRequest<DeleteModuleResult>
    {
        public string ModuleId { get; }

        public DeleteModuleCommand(string moduleId)
        {
            ModuleId = moduleId;
        }
    }
}
