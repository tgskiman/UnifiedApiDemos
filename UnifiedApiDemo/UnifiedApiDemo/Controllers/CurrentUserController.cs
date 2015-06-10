using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using UnifiedApiDemo.Models;

namespace UnifiedApiDemo.Controllers {


  [Authorize]
  public class CurrentUserController : Controller {

    public ActionResult Index() {

        using (var client = new HttpClient()) {
          using (var request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/beta/me")) {
            string token = (string)Session["access_token"];
            request.Headers.Add("Authorization", "Bearer " + token);
            request.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            using (var response = client.SendAsync(request).Result) {
              if (response.StatusCode == HttpStatusCode.OK) {
                string httpResponse = response.Content.ReadAsStringAsync().Result;
                OfficeUser user = JsonConvert.DeserializeObject<OfficeUser>(httpResponse);
                return View(user);
              }

            }
          }
        }
        ViewBag.Message = "Something doesn't seem right";
        return View();      
    }
  }
}