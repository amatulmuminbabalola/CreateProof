using CreateProof.Models;

namespace CreateProof.Interfaces.Repositories
{
    public interface IProjectBriefRepositories
    {
        Task<ProjectBrief?> GetByIdAsync(int id);

        Task<IEnumerable<ProjectBrief>> GetByProjectIdAsync(int projectId);

        Task AddAsync(ProjectBrief projectBrief);

        Task UpdateAsync(ProjectBrief projectBrief);
    }
}