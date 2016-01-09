using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace CFA.Models
{
    public class UserProfileView
    {
                public UserProfileView()
        {
            this.UserProfile = new HashSet<UserProfile>();
            this.webpages_Roles = new HashSet<webpages_Roles>();
        }

        //From UserProfile
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        //From webpages_Membership
        public Nullable<System.DateTime> CreateDate { get; set; }

        public virtual ICollection<UserProfile> UserProfile { get; set; }
        public virtual ICollection<webpages_Membership> webpages_Membership { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}