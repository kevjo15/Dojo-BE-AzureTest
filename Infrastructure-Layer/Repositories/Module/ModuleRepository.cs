using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Layer.Models.ModulModel;
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
        public async Task CreateModuleAsync(ModulModel modul)
        {
            _dojoDBContext.ModuleModel.Add(modul);
            await _dojoDBContext.SaveChangesAsync();
        }

        public async Task DeleteModulesByCourseIdAsync(string courseId)

        {
            var modulesToDelete = _dojoDBContext.ModuleModel.Where(m => m.CourseId == courseId);
            _dojoDBContext.ModuleModel.RemoveRange(modulesToDelete);
            await _dojoDBContext.SaveChangesAsync();
        }




        public async Task<List<ModulModel>> GetAllModulesByCourseId(string courseId)
        {
            var allModulesByCourseId = await (from module in _dojoDBContext.ModuleModel
                                              where (module.CourseId.Equals(courseId))
                                              orderby module.OrderInCourse
                                              select module).ToListAsync();
            return allModulesByCourseId;
        }
        public async Task<ModulModel> GetModuleByIdAsync(string moduleId)
        {
            return await _dojoDBContext.ModuleModel.FindAsync(moduleId);
        }

        public async Task<bool> UpdateModuleAsync(ModulModel updatedModule)
        {
            var module = await _dojoDBContext.ModuleModel.FirstOrDefaultAsync(m => m.ModulId == updatedModule.ModulId);
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
