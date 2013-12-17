
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
    public enum Gender
    {
        [EnumMember]
        Unknown = 0,
        [EnumMember]
        Female = 1,
        [EnumMember]
        Male = 2,
        [EnumMember]
        Complicated = 3
    }

    [Collection("Person")]
    [DataContract]
    public class Person
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [PrimaryKey()]
        public string PersonId { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [DataMember]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [DataMember]
        [Display(Name = "City")]
        public string City { get; set; }

        [DataMember]
        [DisplayFormat(ConvertEmptyStringToNull=true, NullDisplayText = "Share something about yourself.")]
        [Display(Name = "About Me")]
        public string AboutMe { get; set; }

        [DataMember]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "What are your interests?")]
        [Display(Name = "My Interests")]
        public string Interest { get; set; }

    }
}
