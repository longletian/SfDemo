using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SlickOne.WebUtility;

namespace Slickflow.WebDemo.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        [HttpGet]
        public String Test(string id)
        {
            return id;
        }

        [HttpPost,Route("")]
        public String SaveLoginInfo()
        {
            return "Hello World";
        }
    }
}
