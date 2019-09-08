using Newtonsoft.Json;
using System;

namespace AzureDevOpsPOC.Model
{
    public class WorkItemDTO
    {
        public int id { get; set; }
        public int rev { get; set; }
        public Fields fields { get; set; }
        public _Links _links { get; set; }
        public string url { get; set; }

        public class Fields
        {
            [JsonProperty(PropertyName = "System.Id")]
            public int SystemId { get; set; }
            [JsonProperty(PropertyName = "System.WorkItemType")]
            public string SystemWorkItemType { get; set; }
            [JsonProperty(PropertyName = "System.CreatedDate")]
            public DateTime SystemCreatedDate { get; set; }
            [JsonProperty(PropertyName = "System.Title")]
            public string SystemTitle { get; set; }
        }

        public class _Links
        {
            public Self self { get; set; }
            public Workitemupdates workItemUpdates { get; set; }
            public Workitemrevisions workItemRevisions { get; set; }
            public Workitemcomments workItemComments { get; set; }
            public Html html { get; set; }
            public Workitemtype workItemType { get; set; }
            public Fields1 fields { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Workitemupdates
        {
            public string href { get; set; }
        }

        public class Workitemrevisions
        {
            public string href { get; set; }
        }

        public class Workitemcomments
        {
            public string href { get; set; }
        }

        public class Html
        {
            public string href { get; set; }
        }

        public class Workitemtype
        {
            public string href { get; set; }
        }

        public class Fields1
        {
            public string href { get; set; }
        }

    }
}
