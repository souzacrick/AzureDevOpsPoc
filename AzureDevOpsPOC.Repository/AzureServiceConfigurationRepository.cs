using AzureDevOpsPOC.Model;
using System.Linq;

namespace AzureDevOpsPOC.Repository
{
    public class AzureServiceConfigurationRepository
    {
        private readonly AzureDevOpsDbContext _dbContext;

        public AzureServiceConfigurationRepository(AzureDevOpsDbContext context)
        {
            _dbContext = context;
        }

        public AzureServiceConfiguration Get()
        {
            return _dbContext.AzureServiceConfiguration.FirstOrDefault();
        }

        public void Add(AzureServiceConfiguration azureServiceConfiguration)
        {
            _dbContext.Add(azureServiceConfiguration);
            _dbContext.SaveChanges();
        }

        public void Update(AzureServiceConfiguration azureServiceConfiguration)
        {
            _dbContext.Update(azureServiceConfiguration);
            _dbContext.SaveChanges();
        }
    }
}