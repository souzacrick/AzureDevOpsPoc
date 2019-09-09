using System;
using System.Collections.Generic;
using AzureDevOpsPOC.Model;
using AzureDevOpsPOC.Service;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevOpsPOC.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : ControllerBase
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