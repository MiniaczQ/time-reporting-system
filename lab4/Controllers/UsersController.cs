using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using lab4.Models;
using lab4.ActionFilters;

namespace lab4.Controllers;

public class UsersController : BaseController
{
    public UsersController(IMapper mapper) : base(mapper)
    {
    }

    [ReqLoggedOut]
    [HttpGet("all")]
    public IActionResult ListUsers()
    {
        return Ok(Mapper.Map<List<User>>(DbManager.AllUsers()));
    }

    [HttpPost("login")]
    public IActionResult Login(User user)
    {
        Response.Cookies.Append(userNameCookie, null, userNameCookieOpts);
        return Ok();
    }
}
