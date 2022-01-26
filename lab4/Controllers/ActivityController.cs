using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using lab4.Models;
using lab4.ActionFilters;
using lab4.Persistence.Schemas;
using lab4.Utility;

namespace lab4.Controllers;

public class ActivityController : BaseController
{
    public ActivityController(IMapper mapper) : base(mapper)
    {
    }

    [ReqLoggedIn]
    [HttpPost("add")]
    public IActionResult Add(ActivityAdd activity)
    {
        DbManager.AddActivity(LoggedInUser.UserName, activity);
        return Ok();
    }

    [ReqLoggedIn]
    [HttpPatch("edit")]
    public IActionResult Edit(ActivityAll activity)
    {
        DbManager.EditActivity(LoggedInUser.UserName, activity);
        return Ok();
    }

    [ReqLoggedIn]
    [HttpDelete("delete")]
    public IActionResult Delete(ActivityAll activity)
    {
        DbManager.DeleteActivity(LoggedInUser.UserName, activity);
        return Ok();
    }
}
