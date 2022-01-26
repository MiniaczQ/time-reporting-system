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
    [HttpGet("{date:DateTime}")]
    public IActionResult All(DateTime date)
    {
        return Ok(DbManager.Report(LoggedInUser.UserName, date.Date));
    }
}
