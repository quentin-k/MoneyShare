using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoneyShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InternalController : ControllerBase
    {
        [Authorize(Roles = "Admin,Member")]
        public ActionResult Get()
        {
            string name = this.HttpContext.User.Identity.Name;
            return Ok();
        }
    }
}