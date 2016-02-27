using System;
using System.Collections.Generic;
using Arnis.Web.ApiModels;
using Arnis.Web.Models;
using Arnis.Web.Repositiories;
using Microsoft.AspNet.Mvc;

namespace Arnis.Web.ApiControllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public AccountController(IAccountRepository accountRepository, IWorkspaceRepository workspaceRepository)
        {
            _accountRepository = accountRepository;
            _workspaceRepository = workspaceRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] AccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 400;  //400 Bad Request
            }
            else
            {
                //check if account already exists
                var accountExists = _accountRepository.GetByUserName(request.UserName) != null;

                if (!accountExists)
                {
                    //generate a new api key
                    var apiKey = "ARNIS-" + Guid.NewGuid().ToString().ToUpper();
                    var account = new Account
                    {
                        UserName = request.UserName,
                        ApiKey = apiKey
                    };

                    //create client account
                    _accountRepository.Add(account);

                    //create default workspace
                    var workspace = new Workspace
                    {
                        AccountId = account.Id,
                        Name = "default",
                        Description = "This is your default workspace",
                        Owners = new List<string> { account.UserName }
                    };
                    _workspaceRepository.Add(workspace);

                    var response = new
                    {
                        userName = request.UserName,
                        apiKey = apiKey,
                        workspace = new
                        {
                            name = workspace.Name,
                            description = workspace.Description
                        }
                    };

                    return new HttpOkObjectResult(response);
                }
            }

            return new HttpStatusCodeResult(400);
        }

    }
}