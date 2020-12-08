using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutKeeper.Controllers
{
    public class CustomTrainingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
