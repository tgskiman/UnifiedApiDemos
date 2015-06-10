using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UnifiedApiDemo.Utils;

namespace UnifiedApiDemo.Controllers {

  public class AccountController : Controller {

    public void SignIn() {
      if (!Request.IsAuthenticated) {
        // get AuthenticationManager
        IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
        // trigger authentication sequence
        AuthenticationProperties authProperties = new AuthenticationProperties { RedirectUri = "/" };
        string authDefaults = OpenIdConnectAuthenticationDefaults.AuthenticationType; 
        authManager.Challenge(authProperties, authDefaults);
      }
    }

    public void SignOut() {
      string userObjectId = ClaimsPrincipal.Current.FindFirst(SettingsHelper.ClaimTypeObjectIdentifier).Value;
      AuthenticationContext authContext = new AuthenticationContext(SettingsHelper.AzureADAuthority,
                                                                    new EFADALTokenCache(userObjectId));
      authContext.TokenCache.Clear();
      HttpContext.GetOwinContext().Authentication.SignOut(
          OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
    }

    public ActionResult ConsentApp() {
      string resourceId = Request.QueryString["resource"];
      string redirect = Request.QueryString["redirect"];
      string redirectBase = this.Request.Url.GetLeftPart(UriPartial.Authority);
      string redirectUri  = String.Format("{0}/{1}", redirectBase, redirect);

      string authorizationRequest = String.Format(
          "{0}oauth2/authorize?response_type=code&client_id={1}&resource={2}&redirect_uri={3}&prompt={4}",
              Uri.EscapeDataString(SettingsHelper.AzureADAuthority),
              Uri.EscapeDataString(SettingsHelper.ClientId),
              Uri.EscapeDataString(resourceId),
              Uri.EscapeDataString(redirectUri),
              Uri.EscapeDataString("consent")
              );
      return new RedirectResult(authorizationRequest);
    }

    public ActionResult AdminConsentApp() {
      string resourceId = Request.QueryString["resource"];
      string redirect = Request.QueryString["redirect"];
      string redirectBase = this.Request.Url.GetLeftPart(UriPartial.Authority);
      string redirectUri = String.Format("{0}/{1}", redirectBase, redirect);

      string authorizationRequest = String.Format(
          "{0}oauth2/authorize?response_type=code&client_id={1}&resource={2}&redirect_uri={3}&prompt={4}",
              Uri.EscapeDataString(SettingsHelper.AzureADAuthority),
              Uri.EscapeDataString(SettingsHelper.ClientId),
              Uri.EscapeDataString(resourceId),
              Uri.EscapeDataString(redirectUri),
              Uri.EscapeDataString("admin_consent")
              );
      return new RedirectResult(authorizationRequest);
    }

    public void RefreshSession() {
      IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
      string redirectUri = String.Format("/{0}", Request.QueryString["redirect"]);
      authManager.Challenge(new AuthenticationProperties { RedirectUri = redirectUri }, 
                            OpenIdConnectAuthenticationDefaults.AuthenticationType);
    }

  }
}