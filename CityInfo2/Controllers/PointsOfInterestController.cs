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
    }
}
