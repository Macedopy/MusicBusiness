using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MusicBusiness.src.Application.Dtos
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id {get; set;}
    }
}