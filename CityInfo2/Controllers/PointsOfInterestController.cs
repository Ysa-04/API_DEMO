using CityInfo2.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo2.Controllers
{
    [Route("api/cities/{cityId}")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>>
            GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city is null)
            {
                return NotFound();
            }
            return Ok(city.PointsOfInterest);
        }

        //specifieke point of interest ophalen
        [HttpGet("{pointOfInterestId}")] //url voorbeeld: api/cities/1/pointsofinterest/1
        public ActionResult<PointOfInterestDto> 
            GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == pointOfInterestId);

            if (pointOfInterest is null)
            {
                return NotFound();
            }
            return Ok(pointOfInterestId);
        }

        [HttpPost]
        public ActionResult<PointOfInterestForCreationDto> CreatePointOfInterest(int cityId, 
            PointOfInterestForCreationDto pointOfInterest)
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if(city is null)
            {
                return NotFound(); //404
            }

            //id creeeren want geen database dus geen auto increment (womp womp) !niet safe dus!
            var maxPointOfInterestId = CitiesDataStore.Current.Cities
                .SelectMany(c => c.PointsOfInterest)
                .Max(p => p.Id);
            
            var nextId = maxPointOfInterestId + 1; //er gaan mensen zijn (als ge 100 gebruikers hebt ofzo) die dit
                                                   //tegelijk doen en dan kan er een duplicate id gebeuren
            
            //mappen:
            var finalPointOfInterest = new PointOfInterestDto() //point of interest nodig voor intern datamodel!, daarom niet de for creation
            {
                Id = nextId, //wel een id, for creation heeft geen id, want dat wordt pas aangemaakt bij de creatie
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            //201 created
            return CreatedAtAction(nameof(GetPointOfInterest), //action name
                new 
                { cityId = cityId, 
                  pointOfInterestId = finalPointOfInterest.Id 
                }, //route values
                finalPointOfInterest //response body
            );
        }
    }
}
