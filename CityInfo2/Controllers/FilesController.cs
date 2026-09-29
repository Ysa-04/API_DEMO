using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace CityInfo2.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider) : ControllerBase
    {

        [HttpGet("{fileName}")]
        public ActionResult GetFile(string fileName)
        {
            if(!System.IO.File.Exists(fileName))
            {
                return NotFound();
            }

            if(!fileExtensionContentTypeProvider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(fileName);
            return File(bytes, contentType, Path.GetFileName(fileName));
        }
    }
}
