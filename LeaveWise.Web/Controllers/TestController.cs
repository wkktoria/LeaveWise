using LeaveWise.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveWise.Web.Controllers;

public class TestController : Controller
{
    // GET
    public IActionResult Index()
    {
        var data = new TestViewModel
        {
            Name = "Test",
            CreationDate = null
        };
        
        return View(data);
    }
}