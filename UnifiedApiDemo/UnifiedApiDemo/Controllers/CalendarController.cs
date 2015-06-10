using Microsoft.Graph;
using Microsoft.OData.ProxyExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UnifiedApiDemo.Utils;
using UnifiedApiDemo.Models;
using Newtonsoft.Json;

namespace UnifiedApiDemo.Controllers {

  [Authorize]
  public class CalendarController : Controller {
    public async Task<ActionResult> Index() {

      using (var client = new HttpClient()) {
        string restUrl = "https://graph.microsoft.com/beta/me/calendarview?startdatetime={0}&enddatetime={1}";
        DateTime start = DateTime.Today.AddDays(-30);
        DateTime end = DateTime.Today.AddDays(30);
        restUrl = string.Format(restUrl, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));

        using (var request = new HttpRequestMessage(HttpMethod.Get, restUrl)) {
          string token = (string)Session["access_token"];
          request.Headers.Add("Authorization", "Bearer " + token);
          request.Headers.Add("Accept", "application/json;odata.metadata=minimal");

          using (var response = client.SendAsync(request).Result) {
            if (response.StatusCode == HttpStatusCode.OK) {
              string httpResponse = response.Content.ReadAsStringAsync().Result;
              OfficeCalendarEventCollection events = JsonConvert.DeserializeObject<OfficeCalendarEventCollection>(httpResponse);
              return View(events.value);
            }

          }
        }
      }
      ViewBag.Message = "Something doesn't seem right";
      return View();  

      //IEnumerable<IEvent> events = calendarResults.CurrentPage;

      // pass Tenant object to view
      //return View(events);
    }
  }
}