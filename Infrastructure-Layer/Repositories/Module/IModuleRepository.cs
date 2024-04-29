using Domain_Layer.Models.ModulModel;

namespace Infrastructure_Layer.Repositories.Module
{
    public interface IModuleRepository
    {
        Task DeleteModulesByCourseIdAsync(string courseId);
        Task<List<ModulModel>> GetAllModulesByCourseId(string courseId);
        Task CreateModuleAsync(ModulModel modul);
        Task<ModulModel> GetModuleByIdAsync(string moduleId);

    }
}
