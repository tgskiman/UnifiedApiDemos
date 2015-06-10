using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

using UnifiedApiDemo.Utils;
using System.Security.Claims;

namespace UnifiedApiDemo {

  public partial class Startup {
    public void ConfigureAuth(IAppBuilder app) {

      app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
      app.UseCookieAuthentication(new CookieAuthenticationOptions());

      app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions {
        ClientId = SettingsHelper.ClientId,
        Authority = SettingsHelper.AzureADAuthority,
        Notifications = new OpenIdConnectAuthenticationNotifications() {
          // when an auth code is received...
          
          AuthorizationCodeReceived = (context) => {       
     
            // get authentication code 
            string code = context.Code;
      
            // get credentials for the application
            ClientCredential creds = new ClientCredential(SettingsHelper.ClientId, SettingsHelper.ClientSecret);
            
            // get user identity from authentication ticket
            AuthenticationTicket authTicket = context.AuthenticationTicket;
            string userObjectId = authTicket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            // create new token cache object for current user
            EFADALTokenCache userTokenCache = new EFADALTokenCache(userObjectId);

            // create new AuthenticationContext using userTokenCache
            AuthenticationContext authContext = 
              new AuthenticationContext(SettingsHelper.AzureADAuthority, userTokenCache);
            
            // retrieve access token for Office Graph API
            Uri redirectUri = new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path));
            AuthenticationResult authResult = 
              authContext.AcquireTokenByAuthorizationCode(code, redirectUri, creds, SettingsHelper.OfficeGraphResourceId);
            
            // OPTIONAL: cache access token in session variable for direct REST calls
            HttpContext.Current.Session["access_token"] = authResult.AccessToken;
            
            // return result indicating success
            return Task.FromResult(0);
          },
          AuthenticationFailed = (context) => {
            context.HandleResponse();
            return Task.FromResult(0);
          }
        },
        TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters {
          ValidateIssuer = false
        }
      });
    }
  }
}