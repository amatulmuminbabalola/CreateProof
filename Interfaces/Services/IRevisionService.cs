
using CreateProof.Models;

namespace CreateProof.Interfaces.Services
{
    public interface IRevisionService
    {
        Task<List<Revision>> GetAllAsync(int? projectId = null);

        Task<Revision?> GetByIdAsync(int id);

        Task CreateAsync(Revision revision);

        Task UpdateAsync(Revision updatedRevision);
    }
}
