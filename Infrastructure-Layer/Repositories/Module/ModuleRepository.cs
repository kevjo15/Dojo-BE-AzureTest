using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Task DeleteModulesByCourseIdAsync(string courseId)
        {
            throw new NotImplementedException();
        }
    }
}
