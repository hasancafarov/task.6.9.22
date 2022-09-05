using CardTask.Application.Authorization;
using CardTask.Application.Dtos;
using CardTask.Application.Services;
using CardTask.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CardController : BaseController
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<AccountResponse>> GetAll()
        {
            var accounts = _cardService.GetAll();
            return Ok(accounts);
        }

        [HttpGet("{id:int}")]
        public ActionResult<AccountResponse> GetById(int id)
        {
            if (id != User.Id && User.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var account = _cardService.GetById(id);
            return Ok(account);
        }

        [HttpPost]
        public ActionResult<UserResponse> Create(CardCreateRequest model)
        {
            var account = _cardService.Create(model, User.UserName);
            return Ok(account);
        }

        [HttpPut("{id:int}")]
        public ActionResult<UserResponse> Update(int id, CardUpdateRequest model)
        {
            if (id != User.Id && User.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });


            var account = _cardService.Update(id, model);
            return Ok(account);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (id != User.Id && User.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            _cardService.Delete(id);
            return Ok(new { message = "Account deleted successfully" });
        }
    }
    }
