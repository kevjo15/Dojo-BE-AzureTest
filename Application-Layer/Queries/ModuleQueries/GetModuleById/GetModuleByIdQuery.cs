using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application_Layer.Queries.ModuleQueries.GetModuleById
{
    public class GetModuleByIdQuery : IRequest<IActionResult>
    {
        public string ModuleId { get; }

        public GetModuleByIdQuery(string moduleId)
        {
            ModuleId = moduleId;
        }
    }
}
