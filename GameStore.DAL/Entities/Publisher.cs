﻿using GameStore.DAL.Infrastructure.Serializers;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
	[BsonIgnoreExtraElements]
	public class Publisher : BaseEntity
	{
		[StringLength(450)]
		[Index(IsUnique = true)]
		public string CompanyName { get; set; }

		[BsonElement("SupplierID")]
		[NotMapped]
		public int SupplierId { get; set; }

		[BsonElement("Phone")]
		[BsonSerializer(typeof(StringOrInt32ToStringSerializer))]
		[Column(TypeName = "NTEXT")]
		public string Description { get; set; }

		[Column(TypeName = "NTEXT")]
		public string HomePage { get; set; }

		[BsonIgnore]
		public ICollection<Game> Games { get; set; }
	}
}