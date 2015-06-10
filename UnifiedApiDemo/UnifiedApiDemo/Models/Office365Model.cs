
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnifiedApiDemo.Models {

  public class AssignedLicens {
    public List<object> disabledPlans { get; set; }
    public string skuId { get; set; }
  }

  public class AssignedPlan {
    public string assignedTimestamp { get; set; }
    public string capabilityStatus { get; set; }
    public string service { get; set; }
    public string servicePlanId { get; set; }
  }

  public class ProvisionedPlan {
    public string capabilityStatus { get; set; }
    public string provisioningStatus { get; set; }
    public string service { get; set; }
  }

  public class OfficeUser {
    public string objectType { get; set; }
    public string objectId { get; set; }
    public object deletionTimestamp { get; set; }
    public bool accountEnabled { get; set; }
    public List<AssignedLicens> assignedLicenses { get; set; }
    public List<AssignedPlan> assignedPlans { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public string department { get; set; }
    public object dirSyncEnabled { get; set; }
    public string displayName { get; set; }
    public object facsimileTelephoneNumber { get; set; }
    public string givenName { get; set; }
    public object immutableId { get; set; }
    public string jobTitle { get; set; }
    public object lastDirSyncTime { get; set; }
    public string mail { get; set; }
    public string mailNickname { get; set; }
    public object mobile { get; set; }
    public object onPremisesSecurityIdentifier { get; set; }
    public List<object> otherMails { get; set; }
    public string passwordPolicies { get; set; }
    public object passwordProfile { get; set; }
    public string physicalDeliveryOfficeName { get; set; }
    public string postalCode { get; set; }
    public string preferredLanguage { get; set; }
    public List<ProvisionedPlan> provisionedPlans { get; set; }
    public List<object> provisioningErrors { get; set; }
    public List<string> proxyAddresses { get; set; }
    public string sipProxyAddress { get; set; }
    public string state { get; set; }
    public string streetAddress { get; set; }
    public string surname { get; set; }
    public string telephoneNumber { get; set; }
    public string usageLocation { get; set; }
    public string userPrincipalName { get; set; }
    public string userType { get; set; }
  }

  public class UserInfo {
    public string userPrincipalName { get; set; }
    public string objectId { get; set; }
    public string usageLocation { get; set; }
    public string displayName { get; set; }
    public string department { get; set; }
    public string jobTitle { get; set; }
    public string mail { get; set; }
    public string telephoneNumber { get; set; }
    
    public string surname { get; set; }
    public string givenName { get; set; }
    public string streetAddress { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string postalCode { get; set; }
    public string country { get; set; }
    public string photoUrl{ get; set; }   
  }


  public class Body {
    public string ContentType { get; set; }
    public string Content { get; set; }
  }

  public class Location {
    public string DisplayName { get; set; }
  }

  public class EmailAddress {
    public string Address { get; set; }
    public string Name { get; set; }
  }

  public class Status {
    public string Response { get; set; }
    public string Time { get; set; }
  }

  public class Attendee {
    public EmailAddress EmailAddress { get; set; }
    public Status Status { get; set; }
    public string Type { get; set; }
  }

  public class EmailAddress2 {
    public string Address { get; set; }
    public string Name { get; set; }
  }

  public class Organizer {
    public EmailAddress2 EmailAddress { get; set; }
  }


  public class OfficeCalendarEvent {
    public string Id { get; set; }
    public string DateTimeCreated { get; set; }
    public string Subject { get; set; }
    public Body Body { get; set; }
    public string Importance { get; set; }
    public bool HasAttachments { get; set; }
    public DateTime Start { get; set; }
    public string StartTimeZone { get; set; }
    public DateTime End { get; set; }
    public string EndTimeZone { get; set; }
    public Location Location { get; set; }
    public string ShowAs { get; set; }
    public bool IsAllDay { get; set; }
    public bool IsCancelled { get; set; }
    public string Type { get; set; }
    public string SeriesMasterId { get; set; }
    public List<Attendee> Attendees { get; set; }
    public object Recurrence { get; set; }
    public Organizer Organizer { get; set; }
    public string WebLink { get; set; }
  }

  public class OfficeCalendarEventCollection {
    public List<OfficeCalendarEvent> value { get; set; }
  }

}