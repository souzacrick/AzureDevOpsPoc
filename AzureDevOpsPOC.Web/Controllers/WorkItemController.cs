using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("[action]")]
        public List<WorkItem> SortByDateAndFilterByType(string type, bool ascending)
        {
            try
            {
                return ascending ? new WorkItemService().FilterByType(type).OrderBy(x => x.CreatedOn).ToList() : new WorkItemService().FilterByType(type).OrderByDescending(x => x.CreatedOn).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("[action]")]
        public List<WorkItem> GetAllSortByDate(bool ascending)
        {
            try
            {
                return ascending ? new WorkItemService().GetAll().OrderBy(x => x.CreatedOn).ToList() : new WorkItemService().GetAll().OrderByDescending(x => x.CreatedOn).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}