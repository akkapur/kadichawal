
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
        Other = 1,
        [EnumMember]
        SalonSpa = 2,
        [EnumMember]
        GroceryStore = 3,
        [EnumMember]
        Restaurant = 4,
        [EnumMember]
        Technology = 5,
        [EnumMember]
        Construction = 6,
        [EnumMember]
        Supplies = 7,
        [EnumMember]
        Automobile = 8,
        [EnumMember]
        GroceryWala = 9,
        [EnumMember]
        MobileRepair = 10,
        [EnumMember]
        Plumber = 11,
        [EnumMember]
        Electrician = 12,
        [EnumMember]
        Gymnasium = 13,
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
        [IsTextIndexed()]
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
