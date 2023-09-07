using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopDb.Entities;

namespace ShopApi.ControllersMVC
{
    public class ViewController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public ViewController(IWebHostEnvironment environment) => _environment = environment;

        public IActionResult UserView()
            => View("Views\\index.cshtml");

        //[Authorize(Roles = Roles.Admin)]
        [HttpGet("admin")]
        public IActionResult AdminView()
            => View("Views\\adminIndex.cshtml");
    }
}
