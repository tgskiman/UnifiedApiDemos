using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

using UnifiedApiDemo.Data;
using UnifiedApiDemo.Models;

namespace UnifiedApiDemo.Utils {

  public class EFADALTokenCache : TokenCache {

    private AppSecurityContext db = new AppSecurityContext();
    string User;
    PerWebUserCache Cache;

    public EFADALTokenCache(string user) {
      User = user;
      this.AfterAccess = AfterAccessNotification;
      this.BeforeAccess = BeforeAccessNotification;
      this.BeforeWrite = BeforeWriteNotification;
      Cache = db.PerUserCacheList.FirstOrDefault(c => c.webUserUniqueId == User);
      this.Deserialize((Cache == null) ? null : Cache.cacheBits);
    }

    // clear all database entires
    public override void Clear() {
      base.Clear();
      foreach (var cacheEntry in db.PerUserCacheList)
        db.PerUserCacheList.Remove(cacheEntry);
      db.SaveChanges();
    }

    // rehydrate from database if possible
    void BeforeAccessNotification(TokenCacheNotificationArgs args) {
      if (Cache == null) {
        Cache = db.PerUserCacheList.FirstOrDefault(c => c.webUserUniqueId == User);
      }
      else {   // retrieve last write from the DB
        var status = from e in db.PerUserCacheList
                     where (e.webUserUniqueId == User)
                     select new {
                       LastWrite = e.LastWrite
                     };
        if (status.First().LastWrite > Cache.LastWrite) {
          Cache = db.PerUserCacheList.FirstOrDefault(c => c.webUserUniqueId == User);
        }
      }
      this.Deserialize((Cache == null) ? null : Cache.cacheBits);
    }

    // write user updates back to database if neccessary
    void AfterAccessNotification(TokenCacheNotificationArgs args) {
      if (this.HasStateChanged) {
        Cache = new PerWebUserCache {
          webUserUniqueId = User,
          cacheBits = this.Serialize(),
          LastWrite = DateTime.Now
        };
        db.Entry(Cache).State = Cache.EntryId == 0 ? EntityState.Added : EntityState.Modified;
        db.SaveChanges();
        this.HasStateChanged = false;
      }
    }

    void BeforeWriteNotification(TokenCacheNotificationArgs args) {}
  }
}
