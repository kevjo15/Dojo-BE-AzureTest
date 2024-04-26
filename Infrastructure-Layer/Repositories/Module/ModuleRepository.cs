using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Models.ModulModel;
using Infrastructure_Layer.Database;

namespace Infrastructure_Layer.Repositories.Module
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly DojoDBContext _dojoDBContext;
        public ModuleRepository(DojoDBContext dojoDBContext)
        {
            _dojoDBContext = dojoDBContext;
        }

        public async Task CreateModuleAsync(ModulModel modul)
        {
            _dojoDBContext.ModuleModel.Add(modul);
            await _dojoDBContext.SaveChangesAsync();

        }

        public Task DeleteModulesByCourseIdAsync(string courseId)
        {
            throw new NotImplementedException();
        }
    }
}
