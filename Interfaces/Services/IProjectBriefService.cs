using CreateProof.ViewModels.ProjectBriefs;

namespace CreateProof.Interfaces.Services
{
public interface IProjectBriefService
{
Task<ProjectBriefDetailsViewModel?> GetByIdAsync(int id);

    Task<IEnumerable<ProjectBriefDetailsViewModel>> GetByProjectIdAsync(
        int projectId);

    Task<ProjectBriefDetailsViewModel> CreateAsync(
        CreateProjectBriefViewModel model);

    Task<ProjectBriefDetailsViewModel?> UpdateAsync(
        EditProjectBriefViewModel model);

    Task<ProjectBriefDetailsViewModel?> ConfirmAsync(
        ConfirmProjectBriefViewModel model,
        string userId);
}

}