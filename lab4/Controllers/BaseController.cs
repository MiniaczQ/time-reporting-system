using lab4.Persistence;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using lab4.Persistence.Schemas;
using Microsoft.AspNetCore.Mvc.Filters;
using lab4.ActionFilters;

namespace lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        protected readonly DbManager DbManager;
        protected readonly IMapper Mapper;
        private static string userNameCookie = "username";
        protected readonly User LoggedInUser;
        protected BaseController(IMapper mapper)
        {
            DbManager = new DbManager();
            Mapper = mapper;

            if (Request.Cookies.TryGetValue(userNameCookie, out string userName))
            {
                LoggedInUser = DbManager.GetUser(new User { UserName = userName });
                Response.Cookies.Append(userNameCookie, userName, new CookieOptions { HttpOnly = true, MaxAge = TimeSpan.FromMinutes(15) });
            }
            else
            {
                LoggedInUser = null;
                Response.Cookies.Delete(userNameCookie);
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (context.ActionDescriptor.EndpointMetadata.OfType<ReqLoggedIn>().Any())
                if (LoggedInUser != null)
                    context.Result = Forbid();

            if (context.ActionDescriptor.EndpointMetadata.OfType<ReqLoggedOut>().Any())
                if (LoggedInUser == null)
                    context.Result = Forbid();
        }
    }
}
