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
    [Collection("Review")]
    [DataContract]
    public class Review
    {
        [DataMember]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [PrimaryKey()]
        public string ReviewId { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Review Text")]
        [IsTextIndexed()]
        public string ReviewText { get; set; }

        //references the UserId
        [DataMember]
        [Required]
        public string ReviewerId { get; set; }

        [DataMember]
        [Required]
        public string OrganizationId { get; set; }

        [DataMember]
        [Required]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public bool IsApproved { get; set; }
    }
}
