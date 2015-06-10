using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using UnifiedApiDemo.Utils;

namespace UnifiedApiDemo.Controllers {
  public class HomeController : Controller {
    public ActionResult Index() {
      

        return View();
      
    }

    

    public ActionResult About() {
      ViewBag.Message = "Your application description page.";

      return View();
    }


  }
}