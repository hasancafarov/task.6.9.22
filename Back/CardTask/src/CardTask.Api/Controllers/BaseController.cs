using CardTask.Application.Dtos;
using CardTask.Application.Services;
using CardTask.Core.Entities;
using CardTask.DataAccess.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        // returns the current authenticated account (null if not logged in)
        public User User => HttpContext.Items["User"] as User;
        //public IUserService _userService;
       // public User User =>  _userService.GetCurrentUser(UserResponse.UserName); //new CardDbContext().Users.FirstOrDefault(x => x.Id == 3);
      //  public BaseController(IUserService userService)
        
        //    _userService = userService;
        
        //public BaseController() { }
        // private User User => (HttpContext.Items["User"] as User);

    }
}
