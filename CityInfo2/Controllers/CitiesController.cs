using CityInfo2.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo2.Controllers
{
    [ApiController]
    [Route("api/cities")]

    public class CitiesController : ControllerBase //optioneel, want je kunt ook gewoon een class maken en die als controller gebruiken, maar dan heb je geen toegang tot de helper methods van ControllerBase
                                  // controllorbase geeft een paar helper methods zoals Ok(), NotFound(), BadRequest() etc. die je kunt gebruiken om een response terug te sturen naar de client.
    {
        //JsonResult = een ActionResult die een JSON response terugstuurt naar de client. We willen ni altijd JSON terugsturen, dus we gebruiken ActionResult<T> zodat we ook andere types kunnen terugsturen, zoals NotFound() of BadRequest().
        public ActionResult<IEnumerable<CityDto>> GetCities() //IEnumerable is een interface die aangeeft dat het een collectie is, maar je kunt er niet op itereren. List is een concrete implementatie van IEnumerable, dus je kunt er wel op itereren.
        {
            //http get, ge komt binnen in de methode en ik kan heel die database droppen als ik wil
            return Ok(CitiesDataStore.Current.Cities); //ok() is een helper method van controllerbase, die een 200 statuscode terugstuurt met de data die je meegeeft.
            
            /* var temp = new JsonResult(CitiesDataStore.Current.Cities);
            temp.StatusCode = 200; //statuscode 200 is ok, 404 is not found, 500 is internal server error
            return new JsonResult(temp); */
        }
        [HttpGet("{id}")] //parameterbinding, id is een parameter die je meegeeft in de url, bv api/cities/1 -> wordt automatisch achter de url bovenaan toegevoegd
        public ActionResult<CityDto> GetCity(int id)
        {
            var cityToReturn = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == id); //firstordefault: als er geen city is met dat id, dan geeft hij null terug.
            if (cityToReturn == null) {
                return NotFound(); //404
            }
            return Ok(cityToReturn); 
        }

        //
    }
}
