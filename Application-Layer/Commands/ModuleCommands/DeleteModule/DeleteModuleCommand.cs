using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application_Layer.Commands.ModuleCommands.DeleteModule
{
    public class DeleteModuleCommand : IRequest<DeleteModuleResult>
    {
        public string CourseId { get; }

        public DeleteModuleCommand(string courseId)
        {
            CourseId = courseId;
        }
    }
}
