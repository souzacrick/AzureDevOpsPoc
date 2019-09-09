using System;

namespace AzureDevOpsPOC.Model
{
    public class AzureServiceConfiguration
    {
        public int ID { get; set; }
        public int? LastWorkItemID { get; set; }

        public string URL { get; set; }
        public string Organization { get; set; }
        public string AccessToken { get; set; }
        public string Project { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(URL) || string.IsNullOrEmpty(Organization) || string.IsNullOrEmpty(AccessToken) || string.IsNullOrEmpty(Project))
                return false;
            else
                return true;
        }
    }
}