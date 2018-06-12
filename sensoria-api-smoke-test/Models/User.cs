using System;
using System.Collections.Generic;

namespace Sensoria.SmokeTest.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string DisplayName { get; set; }
        public Nullable<byte> DisplayNameTypeId { get; set; }
        public Nullable<byte> DisplayDataTypeId { get; set; }
        public Nullable<DateTime> AgreedTerms { get; set; }
        public Nullable<bool> ShowAvatar { get; set; }
        public string PictureUrl { get; set; }
        public string EmailAddress { get; set; }
        public string IpOrigin { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<DateTime> LastSessionStateChange { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool HasCompletedMandatoryProfile { get; set; }
        public bool EmailOptOut { get; set; }
        public Nullable<int> MaxBulkProcessedRuleId { get; set; }
    }
}