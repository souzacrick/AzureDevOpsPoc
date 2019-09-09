using AzureDevOpsPOC.Model;

namespace AzureDevOpsPOC.Repository
{
    public class WorkItemRepository
    {
        private readonly AzureDevOpsDbContext _dbContext;

        public WorkItemRepository(AzureDevOpsDbContext context)
        {
            _dbContext = context;
        }

        public void Add(WorkItem workItem)
        {
            _dbContext.Add(workItem);
            _dbContext.SaveChanges();
        }
    }
}