
using CreateProof.Interfaces.Repositories;
using CreateProof.Interfaces.Services;
using CreateProof.Models;

namespace CreateProof.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Project project)
        {
            await _repository.AddAsync(project);
            await _repository.SaveChangesAsync();
        }
    }
}
