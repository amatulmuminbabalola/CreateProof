using CreateProof.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateProof.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects => Set<Project>();

        public DbSet<Revision> Revisions => Set<Revision>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<Approval> Approvals => Set<Approval>();
    }
}