namespace PMG.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class AboutUsController : Controller
    {
        public async Task<IActionResult> AboutUs()
        {
            return View();
        }
    }
}