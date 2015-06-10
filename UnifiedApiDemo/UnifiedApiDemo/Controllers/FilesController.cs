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
  public class FilesController : Controller {
    public async Task<ActionResult> Index() {

      // create GraphService object
      GraphService client = GraphServiceFactory.GetGraphService();

      // call across network to Office graph API and wait synchronously for result
      var fileQueryResult = client.Me.files.Query.ExecuteAsync().Result;

      // pass Tenant object to view
      return View(fileQueryResult);
    }
  }
}