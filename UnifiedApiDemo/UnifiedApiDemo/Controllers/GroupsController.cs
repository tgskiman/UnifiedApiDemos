using Microsoft.Graph;
using Microsoft.OData.ProxyExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UnifiedApiDemo.Utils;

namespace UnifiedApiDemo.Controllers {

  [Authorize]
  public class GroupsController : Controller {
    public async Task<ActionResult> Index() {

      // create GraphService object
      GraphService client = GraphServiceFactory.GetGraphService();

      // call across network to Office graph API and wait synchronously for result
      IPagedCollection<IGroup> groupsResult = await client.groups.ExecuteAsync();

      // pull item out of paged collection object
      IEnumerable<IGroup> groups = groupsResult.CurrentPage;

      // pass Tenant object to view
      return View(groups);
    }
  }
}