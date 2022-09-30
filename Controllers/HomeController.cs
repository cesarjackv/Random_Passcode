using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Random_Passcode.Models;

namespace Random_Passcode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        int? visitCount = HttpContext.Session.GetInt32("Num_Generated");
        if(visitCount == null)
        {
            visitCount = 0;
        }
        visitCount++;
        Random rand = new Random();
        string randPass = "";
        string alphaNum = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@!$_-abcdefehijklmnopqrstuvwxyz";
        for(int i = 0; i < 14; i ++){
            int letNum = rand.Next(alphaNum.Length);
            randPass += alphaNum[letNum];
        }
        HttpContext.Session.SetString("Random_Password", randPass);
        string? PassString = HttpContext.Session.GetString("Random_Password");
        HttpContext.Session.SetInt32("Num_Generated", (int)visitCount);
        int? intGen = HttpContext.Session.GetInt32("Num_Generated");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
