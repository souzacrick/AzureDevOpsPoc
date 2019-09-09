using AzureDevOpsPOC.Model;
using AzureDevOpsPOC.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDevOpsPOC.Service
{
    public class WorkItemService
    {
        private readonly WorkItemRepository workItemRepository;

        public WorkItemService()
        {
            workItemRepository = new WorkItemRepository(new AzureDevOpsDbContextFactory().CreateDbContext());
        }

        public List<WorkItem> GetAll()
        {
            return workItemRepository.GetAll();
        }

        public List<WorkItem> FilterByType(string type)
        {
            return workItemRepository.FilterByType(type);
        }
    }
}