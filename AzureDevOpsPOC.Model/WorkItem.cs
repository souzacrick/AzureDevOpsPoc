using Newtonsoft.Json;
using System;

namespace AzureDevOpsPOC.Model
{
    public class WorkItem
    {
        [JsonProperty(PropertyName = "System.Id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "System.WorkItemType")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "System.Title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "System.CreatedDate")]
        public DateTime CreatedOn { get; set; }
    }
}