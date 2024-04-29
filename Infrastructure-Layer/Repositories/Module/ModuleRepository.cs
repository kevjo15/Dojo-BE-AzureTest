using Domain_Layer.Models.ModulModel;
using Infrastructure_Layer.Database;
using Microsoft.EntityFrameworkCore;

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

    }
}
