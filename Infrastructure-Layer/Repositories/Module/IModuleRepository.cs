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
        Task<bool> UpdateModuleAsync(ModulModel module);
        Task<List<ModulModel>> GetAllModulesByCourseId(string courseId);
        Task CreateModuleAsync(ModulModel modul);
        Task<ModulModel> GetModuleByIdAsync(string moduleId);

    }
}
