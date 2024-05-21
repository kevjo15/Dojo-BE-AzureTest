using Infrastructure_Layer.Database;
using Infrastructure_Layer.Repositories.Module;
using MediatR;

namespace Application_Layer.Commands.ModuleCommands.DeleteModule
{
    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand, DeleteModuleResult>
    {
        private readonly IModuleRepository _moduleRepository;

        public DeleteModuleCommandHandler(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<DeleteModuleResult> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _moduleRepository.DeleteModuleByModuleIdAsync(request.ModuleId);
                return new DeleteModuleResult { Success = true, Message = "Modules successfully deleted" };
            }
            catch (Exception ex)
            {
                return new DeleteModuleResult { Success = false, Message = "An error occurred: " + ex.Message };
            }
        }
    }
}
