using AutoMapper;
using Azure.Core;
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

        public async Task DeleteModuleByModuleIdAsync(string moduleId)
        {
            var module = await _dojoDBContext.ModuleModel.FindAsync(moduleId);
            if (module == null)
            {
                throw new Exception($"Module with Id: {moduleId} is not exist in the database");
            }
            _dojoDBContext.ModuleModel.Remove(module);
            await _dojoDBContext.SaveChangesAsync();
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
            var module = await _dojoDBContext.ModuleModel.FindAsync(moduleId);
            if (module == null)
            {
                throw new Exception($"Module with Id: {moduleId} is not exist in the database");
            }
            return module;
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
