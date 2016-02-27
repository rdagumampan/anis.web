using MongoDB.Bson;

namespace Arnis.Web.ApiModels
{
    public class MongoBase
    {
        public ObjectId Id { get; set; }
    }
}