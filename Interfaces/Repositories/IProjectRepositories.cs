
using CreateProof.Models;

namespace CreateProof.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();

        Task<Project?> GetByIdAsync(int id);

        Task AddAsync(Project project);

        Task SaveChangesAsync();
    }
}
