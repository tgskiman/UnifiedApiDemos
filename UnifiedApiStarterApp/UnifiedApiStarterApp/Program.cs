using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using Microsoft.Graph;

namespace UnifiedApiStarterApp {

  class Program {

    #region "Application constants"

    // login authority for Office 365
    const string authority = "https://login.microsoftonline.com/common";

    // TODO - add tenant-specific information
    const string tenantName = "";
    const string tenantDomain = tenantName + ".onMicrosoft.com";

    // TODO: add application-specific information from Azure Active Directory
    const string clientID = "";
    const string redirectUri = "https://localhost/app1";
 
    // Urls for using Office Graph API
    const string resourceOfficeGraphAPI = "https://graph.microsoft.com";
    const string rootOfficeGraphAPI     = "https://graph.microsoft.com/beta/";
    const string urlHostTenancy         = "https://graph.microsoft.com/beta/myOrganization";

    #endregion

    #region "Access token management"

    // add field to cache access token
    protected static string AccessToken = string.Empty;

    // add function to fetch and cache access token
    private static Task<string> AcquireTokenForUser() {

      // fetch access token from for Office Graph API if AccessToken is null
      if (string.IsNullOrEmpty(AccessToken)) {
       
        // create new authentication context
        var authenticationContext = new AuthenticationContext(authority, false);
        
        // use authentication context to trigger user sign-in and return access token
        var userAuthnResult = authenticationContext.AcquireToken(resourceOfficeGraphAPI,
                                                                 clientID,
                                                                 new Uri(redirectUri),
                                                                 PromptBehavior.Auto);
        // cache access token for reuse
        AccessToken = userAuthnResult.AccessToken;
      }
      // return access token to caller
      return Task.FromResult(AccessToken);
    }

    #endregion

    static GraphService client;

    static void Main() {

      //Create an GraphService object by passing in a service root and an access token.
      client = new GraphService(new Uri(urlHostTenancy), AcquireTokenForUser);

      DisplayCurrentUserInfo();
      DisplayFiles();

      Console.WriteLine();
      Console.WriteLine("Press ENTER to end program");
      Console.ReadLine();
    }

    static void DisplayCurrentUserInfo() {
      // call across Internet and wait for response
      IUser user = client.Me.ExecuteAsync().Result;
      Console.WriteLine(user.displayName);
      Console.WriteLine(user.givenName);
      Console.WriteLine(user.surname);
      Console.WriteLine(user.mail);
    }

    static void DisplayFiles() {
      var files = client.Me.files.Query.ExecuteAsync().Result;
      foreach (Microsoft.Graph.Item item in files) {
        Console.WriteLine(item.name);
        Console.WriteLine(item.type);
        Console.WriteLine();
      }

    }

    
  }
}
