using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GroupMeetingASP.NETCoreWebApp.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Demo()
        {
            return View();
        }


    }
}