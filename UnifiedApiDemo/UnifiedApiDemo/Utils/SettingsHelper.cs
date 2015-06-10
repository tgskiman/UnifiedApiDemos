using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UnifiedApiDemo.Utils {

  public class SettingsHelper {

    public static string ClientId { get { return ConfigurationManager.AppSettings["ida:ClientID"]; } }

    public static string ClientSecret { get { return ConfigurationManager.AppSettings["ida:Password"]; } }

    public static string AzureAdTenantId { get { return ConfigurationManager.AppSettings["ida:TenantId"]; } }

    public static string AzureADAuthority {
      get { return string.Format("https://login.windows.net/{0}/", AzureAdTenantId); }
    }

    public static string OfficeGraphResourceId {
      get { return "https://graph.microsoft.com/"; }
    }

    public static string OfficeGraphServiceEndpoint {
      get { return "https://graph.microsoft.com/beta/myOrganization"; }
    }

    public static string ClaimTypeObjectIdentifier {
      get { return "http://schemas.microsoft.com/identity/claims/objectidentifier"; }
    }
  }

}