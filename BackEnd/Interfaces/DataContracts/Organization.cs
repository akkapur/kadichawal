
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
    [DataContract]
    public enum BusinessType
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        Salon = 1,
        [EnumMember]
        GroceryStore = 2,
        [EnumMember]
        Restaurant = 3,
        [EnumMember]
        Technology = 4,
        [EnumMember]
        Construction = 5,
        [EnumMember]
        Supplies = 6,
        [EnumMember]
        Other = 7
    }

    [Collection("Organization")]
    [DataContract]
    public class Organization
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [PrimaryKey()]
        public string OrganizationId { get; set; }
        [DataMember]
        [Required]
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }
        [DataMember]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [DataMember]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }
        [DataMember]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }
        [DataMember]
        [Required]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        [Display(Name = "Type of Business")]
        public BusinessType BusinessType { get; set; }
        [DataMember]
        [Display(Name = "Hours")]
        public string BusinessHours { get; set; }
        [DataMember]
        [Display(Name = "Contact Person")]
        public Person ContactPerson { get; set; }
        [DataMember]
        public string CreatedById { get; set; }
    }
}
