using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTracking.Controllers
{
    public class BasketProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
