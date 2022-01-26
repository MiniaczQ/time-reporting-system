using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using lab4.Models;
using lab4.ActionFilters;
using lab4.Persistence.Schemas;
using lab4.Utility;

namespace lab4.Controllers;

public class ProjectController : BaseController
{
    public ProjectController(IMapper mapper) : base(mapper)
    {
    }

    [ReqLoggedIn]
    [HttpGet("all")]
    public IActionResult Projects()
    {
        return Ok(DbManager.Projects());
    }

    [ReqLoggedIn]
    [HttpGet("{projectCode}/subcodes")]
    public IActionResult SubprojectCodes(string projectCode)
    {
        return Ok(DbManager.SubprojectCodes(projectCode));
    }
}
