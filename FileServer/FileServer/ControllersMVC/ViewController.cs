using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopDb.Entities;
using ShopDb.Enums;

namespace FileServer.ControllersMVC
{
    public class ViewController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public ViewController(IWebHostEnvironment environment) => _environment = environment;

        public IActionResult UserView()
            => PhysicalFile(Path.Combine(_environment.WebRootPath, Path.Combine("Views", "index.html")), "text/html");

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("admin")]
        public IActionResult AdminView()
            => PhysicalFile(Path.Combine(_environment.WebRootPath, Path.Combine("Views", "adminIndex.html")), "text/html");
    }
}
