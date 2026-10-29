using CreateProof.Data;
using CreateProof.Interfaces.Repositories;
using CreateProof.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateProof.Repositories
{
    public class ProjectBriefRepositories : IProjectBriefRepositories
    {
        private readonly ApplicationDbContext _context;

        public ProjectBriefRepositories(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectBrief?> GetByIdAsync(int id)
        {
            return await _context.Set<ProjectBrief>()
                .FirstOrDefaultAsync(pb => pb.Id == id);
        }

        public async Task<IEnumerable<ProjectBrief>> GetByProjectIdAsync(
            int projectId)
        {
            return await _context.Set<ProjectBrief>()
                .AsNoTracking()
                .Where(pb => pb.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task AddAsync(ProjectBrief projectBrief)
        {
            await _context.Set<ProjectBrief>().AddAsync(projectBrief);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProjectBrief projectBrief)
        {
            _context.Set<ProjectBrief>().Update(projectBrief);

            await _context.SaveChangesAsync();
        }
    }
}