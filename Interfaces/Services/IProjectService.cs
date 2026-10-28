
using CreateProof.Models;

namespace CreateProof.Interfaces.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllAsync();

        Task<Project?> GetByIdAsync(int id);

        Task CreateAsync(Project project);
    }
}
