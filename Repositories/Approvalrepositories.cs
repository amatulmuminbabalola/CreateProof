using CreateProof.Interfaces.Repositories;
using CreateProof.Models;
using Microsoft.EntityFrameworkCore;
using CreateProof.Data;

namespace CreateProof.Repositories
{
public class ApprovalRepository : IApprovalRepository
{
private readonly ApplicationDbContext _context;

    public ApprovalRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Approval>> GetAllAsync()
    {
        return await _context.Set<Approval>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Approval?> GetByIdAsync(int id)
    {
        return await _context.Set<Approval>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Approval>> GetByProjectIdAsync(int projectId)
    {
        return await _context.Set<Approval>()
            .AsNoTracking()
            .Where(a => a.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task AddAsync(Approval approval)
    {
        await _context.Set<Approval>().AddAsync(approval);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Approval approval)
    {
        _context.Set<Approval>().Update(approval);
        await _context.SaveChangesAsync();
    }
}

}