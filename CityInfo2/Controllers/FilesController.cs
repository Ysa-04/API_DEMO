using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace CityInfo2.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider) : ControllerBase //constructor injection van FileExtensionContentTypeProvider
    {

        [HttpGet("{fileName}")]
        //async ga je enkel gebruiken bij IO (input output) operaties,
        //zoals het lezen van een bestand of het ophalen van gegevens uit een database. => TRAAG, vandaar async
        //Het gebruik van async zorgt ervoor dat de thread niet geblokkeerd wordt terwijl de IO-operatie wordt uitgevoerd, 
        //waardoor de applicatie responsiever blijft.

        //async = modifier die aangeeft dat de methode asynchroon is en een Task of Task<T> retourneert.
        //Awair = operator die wordt gebruikt om te wachten op de voltooiing van een asynchrone taak voordat de uitvoering van de code wordt voortgezet.
        public async Task<ActionResult> GetFile(string fileName) //async returns een Task of een Task<T> (in dit geval ActionResult) en kan await gebruiken om te wachten op asynchrone operaties.
        {
            if(!System.IO.File.Exists(fileName))
            {
                return NotFound();
            }

            if(!fileExtensionContentTypeProvider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(fileName); 
            return File(bytes, contentType, Path.GetFileName(fileName));
        }
    }
}
