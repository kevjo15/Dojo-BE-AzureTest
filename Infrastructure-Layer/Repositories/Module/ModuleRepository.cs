using AutoMapper;
using Domain_Layer.Models.Module;
using Infrastructure_Layer.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Repositories.Module
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly DojoDBContext _dojoDBContext;
        private readonly IMapper _mapper;
        public ModuleRepository(DojoDBContext dojoDBContext, IMapper mapper)
        {
            _dojoDBContext = dojoDBContext;
            _mapper = mapper;
        }
        public async Task CreateModuleAsync(ModuleModel modul)
        {
            _dojoDBContext.ModuleModel.Add(modul);
            await _dojoDBContext.SaveChangesAsync();
        }

        public Task DeleteModulesByCourseIdAsync(string courseId)
        {
            //var modulesToDelete = _dojoDBContext.ModuleModel.Where(m => m.CourseId == courseId);
            //_dojoDBContext.ModuleModel.RemoveRange(modulesToDelete);
            //await _dojoDBContext.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<List<ModuleModel>> GetAllModulesByCourseId(string courseId)
        {
            var allModulesByCourseId = await _dojoDBContext.CourseHasModules
                                            .Where(cm => cm.Course.CourseId == courseId)
                                            .Select(cm => cm.Module)
                                            .OrderBy(cm => cm.OrderInCourse)
                                            .ToListAsync();
            return allModulesByCourseId;
        }
        public async Task<ModuleModel> GetModuleByIdAsync(string moduleId)
        {
            return await _dojoDBContext.ModuleModel.FindAsync(moduleId);
        }

        public async Task<bool> UpdateModuleAsync(ModuleModel updatedModule)
        {
            var module = await _dojoDBContext.ModuleModel.FirstOrDefaultAsync(m => m.ModuleId == updatedModule.ModuleId);
            if (module == null)
            {
                return false;
            }

            _mapper.Map(updatedModule, module);

            await _dojoDBContext.SaveChangesAsync();
            return true;
        }
    }
}
