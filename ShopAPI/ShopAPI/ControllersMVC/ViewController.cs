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
            => PhysicalFile(Path.Combine(_environment.WebRootPath, "index.html"), "text/html");

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("admin")]
        public IActionResult AdminView()
            => PhysicalFile(Path.Combine(_environment.WebRootPath, "adminIndex.html"), "text/html");
    }
}
