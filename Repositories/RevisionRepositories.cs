
using CreateProof.Data;
using CreateProof.Interfaces.Repositories;
using CreateProof.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateProof.Repositories
{
    public class RevisionRepository : IRevisionRepository
    {
        private readonly ApplicationDbContext _context;

        public RevisionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Revision>> GetAllAsync()
        {
            return await _context.Revisions.ToListAsync();
        }

        public async Task<Revision?> GetByIdAsync(int id)
        {
            return await _context.Revisions
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Revision revision)
        {
            await _context.Revisions.AddAsync(revision);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
