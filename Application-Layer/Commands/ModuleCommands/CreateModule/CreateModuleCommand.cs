using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.DTO_s;
using Application_Layer.DTO_s.Module;
using Domain_Layer.Models.ModulModel;
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
