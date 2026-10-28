
using CreateProof.Models;

namespace CreateProof.Interfaces.Repositories
{
    public interface IRevisionRepository
    {
        Task<List<Revision>> GetAllAsync();

        Task<Revision?> GetByIdAsync(int id);

        Task AddAsync(Revision revision);

        Task SaveChangesAsync();
    }
}
