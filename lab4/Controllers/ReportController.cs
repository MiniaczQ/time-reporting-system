using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using lab4.Models;
using lab4.ActionFilters;
using lab4.Persistence.Schemas;
using lab4.Utility;

namespace lab4.Controllers;

public class ReportController : BaseController
{
    public ReportController(IMapper mapper) : base(mapper)
    {
    }

    [ReqLoggedIn]
    [HttpGet("activities/{date:DateTime}")]
    public IActionResult ActivitiesReport(DateTime date)
    {
        return Ok(DbManager.ActivitiesReport(LoggedInUser.UserName, date.Date));
    }

    [ReqLoggedIn]
    [HttpGet("accepted_activities")]
    public IActionResult AcceptedActivitiesReport()
    {
        return Ok(DbManager.AcceptedActivitiesReport(LoggedInUser.UserName));
    }
}
