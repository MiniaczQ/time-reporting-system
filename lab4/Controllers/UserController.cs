using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using lab4.Models;
using lab4.ActionFilters;
using lab4.Persistence.Schemas;
using lab4.Utility;

namespace lab4.Controllers;

public class UserController : BaseController
{
    public UserController(IMapper mapper) : base(mapper)
    {
    }

    [ReqLoggedOut]
    [HttpGet("all")]
    public IActionResult ListUsers()
    {
        return Ok(Mapper.Map<List<UserAll>>(DbManager.AllUsers()));
    }

    [ReqLoggedOut]
    [HttpPost("login")]
    public IActionResult Login(UserAll user)
    {
        if (DbManager.IsUser(Mapper.Map<User>(user)))
        {
            Response.Cookies.Append(userNameCookie, user.UserName, userNameCookieOpts);
            return Ok();
        }
        else
        {
            return Unauthorized();
        }
    }

    [ReqLoggedOut]
    [HttpPost("register")]
    public IActionResult Register(UserAll user)
    {
        try
        {
            DbManager.AddUser(Mapper.Map<User>(user));
            Response.Cookies.Append(userNameCookie, user.UserName, userNameCookieOpts);
            return Ok();
        }
        catch (System.Exception)
        {
            return Conflict();
        }
    }

    [ReqLoggedIn]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(userNameCookie);
        return Ok();
    }

    [HttpGet("me")]
    public IActionResult Me()
    {
        if (LoggedInUser != null)
            return Ok(Mapper.Map<UserAll>(LoggedInUser));
        else
            return Ok();
    }
}
