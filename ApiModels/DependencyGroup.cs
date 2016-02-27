using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Arnis.Web.ApiModels
{
    public class DependencyGroup: MongoBase
    {
        [BsonElement("targetFramework")]
        public string TargetFramework{ get; set; }

        [BsonElement("dependencies")]
        public List<DependencyInfo> Dependencies { get; } = new List<DependencyInfo>();
    }
}