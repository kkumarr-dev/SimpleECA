using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleECA.WEB.Controllers
{
    public class BsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult modal()
        {
            return View();
        }
    }
}
