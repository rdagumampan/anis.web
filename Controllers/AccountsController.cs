using Arnis.Web.Repositiories;
using Microsoft.AspNet.Mvc;

namespace Arnis.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("{userName}")]
        public IActionResult GetAccount(string userName)
        {
            var account = _accountRepository.GetByUserName(userName);
            if (null != account)
            {
                return View(account);
            }
            else
            {
                return View();
            }
        }
    }
}