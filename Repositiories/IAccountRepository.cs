using System.Threading.Tasks;
using Arnis.Documents;

namespace Arnis.Web.Repositiories
{
    public interface IAccountRepository
    {
        Task Create(Account account);
        Account GetByUserName(string userName);
        Account GetByApiKey(string apiKey);
    }
}