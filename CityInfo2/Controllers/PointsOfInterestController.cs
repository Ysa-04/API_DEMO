using CityInfo2.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo2.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
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
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, 
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
                { 
                  cityId, 
                  pointOfInterestId = finalPointOfInterest.Id 
                }, //route values
                finalPointOfInterest //response body
            );
        }

        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(int cityId,
            int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city is null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == pointOfInterestId);
            if (pointOfInterestFromStore is null)
            {
                return NotFound();
            }
            city.PointsOfInterest.Remove(pointOfInterestFromStore);
            return NoContent(); //204

            /* je wilt gewoon checken of alles wel bestaat da je wilt deleten 
               (anders stuur je 404 not found), als alles werkt en je object word verwijdert 
               dan stuur je een 204 (succes en geeft een lege body terug) */
        }

        [HttpPut("{pointOfInterestId}")]
        public ActionResult UpdatePointOfInterest(int cityId,
        int pointOfInterestId,
        PointOfInterestForUpdateDto pointOfInterest) //content of request body (json) word automatisch gemapped naar dit dto object
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city is null)
            {
                return NotFound();
            }

            // find point of interest
            var pointOfInterestFromStore = city.PointsOfInterest
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore is null)
            {
                return NotFound();
            }
            //mappen van de properties van de incoming dto naar de bestaande entity
            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent(); //204
        }

        [HttpPatch("{pointOfInterestId}")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
        JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            // check if city exists
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city is null)
            {
                return NotFound();
            }
            // check if point of interest exists
            // waarom fromStore? want we willen de bestaande entity updaten, niet een nieuwe aanmaken
            var pointOfInterestFromStore = city.PointsOfInterest
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore is null)
            {
                return NotFound();
            }
            // create a new instance of the DTO to hold the updated values
            var pointOfInterestToPatch =
                new PointOfInterestForUpdateDto() // create a new instance of the DTO to hold the updated values
                {
                    Name = pointOfInterestFromStore.Name, 
                    Description = pointOfInterestFromStore.Description
                    // initialize the DTO with the current values from the store
                };

            // apply the patch document to the DTO
            patchDocument.ApplyTo(pointOfInterestToPatch, jsonPatchError =>
            {
                var key = jsonPatchError.AffectedObject.GetType().Name; // get the name of the affected object type 
                ModelState.AddModelError(key, jsonPatchError.ErrorMessage); //key is the name of the affected object type, and the error message is added to the model state
            });

            // validate the patched DTO
            if (!ModelState.IsValid)
            {
                // if the model state is invalid, return a bad request response with the model state errors
                return BadRequest(ModelState);
            }

            // validate the patched DTO using TryValidateModel
            if (!TryValidateModel(pointOfInterestToPatch))
            {
                //  if the model state is invalid, return a bad request response with the model state errors
                return BadRequest(ModelState);
            }

            // update the point of interest in the store with the patched values
            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
            return NoContent();
        }
    }
}
