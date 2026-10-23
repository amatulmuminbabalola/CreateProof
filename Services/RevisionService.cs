
using CreateProof.Interfaces.Repositories;
using CreateProof.Interfaces.Services;
using CreateProof.Models;

namespace CreateProof.Services
{
    public class RevisionService : IRevisionService
    {
        private readonly IRevisionRepository _repository;

        public RevisionService(IRevisionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Revision>> GetAllAsync(
            int? projectId = null)
        {
            var revisions = await _repository.GetAllAsync();

            if (projectId.HasValue)
            {
                revisions = revisions
                    .Where(r => r.ProjectId == projectId.Value)
                    .ToList();
            }

            return revisions;
        }

        public async Task<Revision?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Revision revision)
        {
            revision.RequestedAt = DateTime.UtcNow;

            await _repository.AddAsync(revision);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Revision updatedRevision)
        {
            var revision = await _repository.GetByIdAsync(
                updatedRevision.Id);

            if (revision == null)
            {
                throw new KeyNotFoundException("Revision not found.");
            }

            revision.Description = updatedRevision.Description;
            revision.Status = updatedRevision.Status;

            revision.CompletedAt = revision.Status == "Completed"
                ? DateTime.UtcNow
                : null;

            await _repository.SaveChangesAsync();
        }
    }
}
