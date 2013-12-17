
using Interfaces.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DataContracts
{
    [Collection("User")]
    [DataContract]
    public class User
    {
        private string _emailAddress;

        [DataMember]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [PrimaryKey()]
        public string UserId { get; set; }

        [DataMember]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataMember]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataMember]
        [Display(Name = "Password Question")]
        public string PasswordQuestion { get; set; }

        [DataMember]
        [Display(Name = "Password Answer")]
        public string PasswordAnswer { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress 
        {
            get { return _emailAddress; }
            set { UserName = value; _emailAddress = value; }
        }

        [DataMember]
        public bool IsOrganization { get; set; }

        public Guid RoleId { get; set; }

        public bool IsApproved {get; set;}

        public DateTime LastActivityDate {get; set;}
        
        public DateTime LastLoginDate {get; set;}
        
        public DateTime LastPasswordChangedDate {get; set;}
        
        public DateTime CreationDate {get; set;} 
        
        public bool IsOnLine {get; set;}

        public bool IsLockedOut { get; set; }

        public DateTime LastLockedOutDate { get; set; }
        
        public Int32 FailedPasswordAttemptCount {get; set;}
        
        public DateTime FailedPasswordAttemptWindowStart {get ;set;}
        
        public Int32 FailedPasswordAnswerAttemptCount {get; set;}
        
        public DateTime FailedPasswordAnswerAttemptWindowStart {get; set;}
    }
}
