using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FrameWork.MongoDB.MongoDbConfig
{
    public class MongoEntity
    {
        public MongoEntity()
        {
            _id = new ObjectId(Guid.NewGuid().ToString("N"));
        }
        [BsonId]
        public ObjectId _id { get; set; }
    }
}
