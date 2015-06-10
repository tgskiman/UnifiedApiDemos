using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UnifiedApiDemo.Utils;

using Microsoft.Graph;
using Microsoft.OData.ProxyExtensions;

namespace UnifiedApiDemo.Controllers {

  [Authorize]
  public class TenantController : Controller {

    public async Task<ActionResult> Index() {

      // create GraphService object
      GraphService client = GraphServiceFactory.GetGraphService();
      
      // call across network to Office graph API and wait synchronously for result
      IPagedCollection<ITenantDetail> tenantDetailsResult = await client.tenantDetails.ExecuteAsync();

      // pull item out of paged collection object
      ITenantDetail tenantDetail = tenantDetailsResult.CurrentPage.FirstOrDefault();
      
      // pass Tenant object to view
      return View(tenantDetail);
    }

  }
}

