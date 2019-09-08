using AzureDevOpsPOC.Model;
using AzureDevOpsPOC.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDevOpsPOC.Application
{
    public class AzureDevOpsService
    {
        private readonly AzureServiceConfigurationRepository _azureServiceConfigurationRepository;

        public AzureDevOpsService(AzureServiceConfigurationRepository azureServiceConfigurationRepository)
        {
            _azureServiceConfigurationRepository = azureServiceConfigurationRepository;
        }

        public AzureServiceConfiguration GetServiceConfiguration()
        {
            return _azureServiceConfigurationRepository.Get();
        }
    }
}
