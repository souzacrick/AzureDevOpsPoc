using System;

namespace AzureDevOpsPOC.Model
{
    public class WorkItem
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}