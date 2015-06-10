using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.Graph;
using UnifiedApiDemo.Utils;
using UnifiedApiDemo.Models;
using System.Threading.Tasks;

namespace UnifiedApiDemo.Controllers {

  [Authorize]
  public class UsersController : Controller {

    public async Task<ActionResult> Index() {

      GraphService client = GraphServiceFactory.GetGraphService();

      var usersResult = await client.users.Where(u => u.surname.StartsWith("J")).ExecuteAsync();

      List<UserInfo> users = new List<UserInfo>();

      foreach (User user in usersResult.CurrentPage.Where(u => !string.IsNullOrEmpty(u.usageLocation))) {
        UserInfo userInfo = new UserInfo {
          userPrincipalName = user.userPrincipalName,
          objectId = user.objectId,
          usageLocation = user.usageLocation,
          displayName = user.displayName,
          department = user.department,
          jobTitle = user.jobTitle,
          mail = user.mail,
          telephoneNumber = user.telephoneNumber,
          givenName = user.givenName,
          surname = user.surname,
          streetAddress = user.streetAddress,
          city = user.city,
          state = user.state,
          postalCode = user.postalCode
          // photoUrl = user.UserPhoto.Id
        };
        users.Add(userInfo);

      }

      return View(users);
    }
  }

}