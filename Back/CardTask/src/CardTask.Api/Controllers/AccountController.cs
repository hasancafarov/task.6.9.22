using CardTask.Application.Authorization;
using CardTask.Application.Dtos;
using CardTask.Application.Services;
using CardTask.Core.Entities;
using CardTask.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController:BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<AccountResponse>> GetAll()
        {
            var accounts = _accountService.GetAll();
            return Ok(accounts);
        }

        [HttpGet("{id:int}")]
        public ActionResult<AccountResponse> GetById(int id)
        {
            // users can get their own account and admins can get any account
            if (id != User.Id && User.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var account = _accountService.GetById(id);
            return Ok(account);
        }

        [HttpPost]
        public ActionResult<UserResponse> Create(AccountCreateRequest model)
        {
            var account = _accountService.Create(model,User.UserName);
            return Ok(account);
        }

        [HttpPut("{id:int}")]
        public ActionResult<UserResponse> Update(int id, AccountUpdateRequest model)
        {
            // users can update their own account and admins can update any account
            if (id != User.Id && User.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            // only admins can update role
            //if (User.Role != Role.Admin)
              //  model.Role = null;

            var account = _accountService.Update(id, model);
            return Ok(account);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            // users can delete their own account and admins can delete any account
            if (id != User.Id && User.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            _accountService.Delete(id);
            return Ok(new { message = "Account deleted successfully" });
        }
    }
}
