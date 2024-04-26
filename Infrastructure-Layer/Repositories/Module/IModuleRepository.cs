using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Models.ModulModel;

namespace Infrastructure_Layer.Repositories.Module
{
    public interface IModuleRepository
    {
        Task DeleteModulesByCourseIdAsync(string courseId);
        Task CreateModuleAsync(ModulModel modul);
    }
}
