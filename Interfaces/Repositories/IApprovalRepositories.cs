using CreateProof.Models;

namespace CreateProof.Interfaces.Repositories
{
    public interface IApprovalRepository
    {
        Task<IEnumerable<Approval>> GetAllAsync();

        Task<Approval?> GetByIdAsync(int id);

        Task<IEnumerable<Approval>> GetByProjectIdAsync(int projectId);

        Task AddAsync(Approval approval);

        Task UpdateAsync(Approval approval);
    }
}