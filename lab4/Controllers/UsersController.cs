using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using lab4.Models;

namespace lab4.Controllers;

public class UsersController : BaseController
{
    public UsersController(IMapper mapper) : base(mapper)
    {
    }

    [HttpGet("all")]
    public IActionResult ListUsers()
    {
        return Ok(Mapper.Map<List<User>>(DbManager.AllUsers()));
    }

    [HttpPost("login")]
    public IActionResult Login(User user)
    {


        return Ok(Mapper.Map<List<User>>(DbManager.AllUsers()));
    }
}
