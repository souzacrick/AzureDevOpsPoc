using AzureDevOpsPOC.Model;
using Microsoft.EntityFrameworkCore;

namespace AzureDevOpsPOC.Repository
{
    public class AzureDevOpsDbContext : DbContext
    {
        public DbSet<WorkItem> WorkItem { get; set; }
        public DbSet<AzureServiceConfiguration> AzureServiceConfiguration { get; set; }

        public AzureDevOpsDbContext(DbContextOptions<AzureDevOpsDbContext> options) : base(options)
        {

        }
    }
}