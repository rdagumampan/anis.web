using Arnis.Web.ApiModels;
using MongoDB.Bson;

namespace Arnis.Web.Repositiories
{
    public interface IAccountRepository
    {
        Account GetByUserName(string userName);
        Account GetByApiKey(string apiKey);
        Account GetById(ObjectId id);
        void Add(Account account);
    }
}