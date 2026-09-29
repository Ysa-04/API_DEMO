using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace CityInfo2.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        public FilesController()
        {

        }

        [HttpGet("{fileName}")]
        public ActionResult GetFile(string fileName)
        {
            if(!System.IO.File.Exists(fileName))
            {
                return NotFound();
            }

            var bytes = System.IO.File.ReadAllBytes(fileName);
            return File(bytes, "text/plain", Path.GetFileName(fileName));
        }
    }
}
