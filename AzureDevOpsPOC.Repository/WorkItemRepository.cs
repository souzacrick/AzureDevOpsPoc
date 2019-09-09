using AzureDevOpsPOC.Model;
using System.Collections.Generic;
using System.Linq;

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

        public List<WorkItem> GetAll()
        {
            return _dbContext.WorkItem.ToList();
        }
    }
}