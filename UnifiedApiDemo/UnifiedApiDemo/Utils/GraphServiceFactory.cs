using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace UnifiedApiDemo.Utils {

  public class GraphServiceFactory {

    public static GraphService GetGraphService() {
      
      // get userObjectId for current user
      string signInUserId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
      string userObjectId = ClaimsPrincipal.Current.FindFirst(SettingsHelper.ClaimTypeObjectIdentifier).Value;
      UserIdentifier userIdentifier = new UserIdentifier(userObjectId, UserIdentifierType.UniqueId);

      // create client credential
      ClientCredential clientCredential = new ClientCredential(SettingsHelper.ClientId, SettingsHelper.ClientSecret);

      // create AuthenticationContext
      AuthenticationContext authContext = new AuthenticationContext(SettingsHelper.AzureADAuthority, 
                                                                    new EFADALTokenCache(signInUserId));
      // create GraphService object to access Office Graph API
      return new GraphService(new Uri(SettingsHelper.OfficeGraphServiceEndpoint),
        () => {
          // retrieve AuthenticationResult with access Token
          AuthenticationResult authResult = 
            authContext.AcquireTokenSilent(SettingsHelper.OfficeGraphResourceId, clientCredential, userIdentifier);
          // return AccessToken
          return Task.FromResult<string>(authResult.AccessToken);
        });
    }
  }
}
