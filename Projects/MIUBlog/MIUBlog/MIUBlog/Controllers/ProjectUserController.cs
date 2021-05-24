using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIUBlog.WebUI.Controllers
{
    public class ProjectUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
