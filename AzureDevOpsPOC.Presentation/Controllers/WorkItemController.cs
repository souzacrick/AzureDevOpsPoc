using System;
using System.Collections.Generic;
using AzureDevOpsPOC.Model;
using AzureDevOpsPOC.Service;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevOpsPOC.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class WorkItemController : Controller
    {
        [HttpGet("[action]")]
        public List<WorkItem> GetAll()
        {
            try
            {
                return new WorkItemService().GetAll();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}