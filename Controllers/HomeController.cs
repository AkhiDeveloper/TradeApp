using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TradeApp.Models;

namespace TradeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Redirect("/Identity/GeneralDashBoard");
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Python()
        {
            Microsoft.Scripting.Hosting.ScriptEngine pythonEngine = IronPython.Hosting.Python.CreateEngine();
            Microsoft.Scripting.Hosting.ScriptSource pythonScript = pythonEngine.CreateScriptSourceFromFile("C:\\Users\\Alok Mishra\\source\repos\\AkhiDeveloper\\TradeApp\\Python\\recommends.py");
            
            Microsoft.Scripting.Hosting.ScriptScope scope = pythonEngine.CreateScope();
            scope.SetVariable("u_id", "How do you do?");
            
            pythonScript.Execute(scope);
            
            System.Console.Out.WriteLine("output: " + scope.GetVariable("output"));
            var output = scope.GetVariable("output");
            return View();
        }
    }
}
