using BirdObservationLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdObservationREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdObservationsController : ControllerBase
    {
        private readonly BirdObservationsRepository _birdObservationsRepository;

        // Constructor der initialiserer BirdObservationsRepository
        public BirdObservationsController(BirdObservationsRepository birdObservationsRepository)
        {
            _birdObservationsRepository = birdObservationsRepository;
        }

        // Get api/birdobservations
        // Henter alle birdobservations
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<IEnumerable<BirdObservation>> GetAll()
        {
            IEnumerable<BirdObservation> birdObservations = _birdObservationsRepository.GetAll();
            return Ok(birdObservations);
        }

        // Get api/birdobservations/{id}
        // Henter en birdobservation ud fra id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<BirdObservation> GetById(int id)
        {
            BirdObservation? birdObservation = _birdObservationsRepository.GetById(id);
            if (birdObservation == null)
            {
                return NotFound();
            }
            return Ok(birdObservation);
        }

        // Post api/birdobservations
        // Opretter en ny birdobservation
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<BirdObservation> Post([FromBody] BirdObservation birdObservation)
        {
            try
            {
                BirdObservation createdBirdObservation = _birdObservationsRepository.Add(birdObservation);
                return Created("/" + createdBirdObservation.Id, createdBirdObservation);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete api/birdobservations/{id}
        // Sletter en birdobservation ud fra id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<BirdObservation> Delete(int id)
        {
            BirdObservation? deletedBirdObservation = _birdObservationsRepository.Delete(id);
            if (deletedBirdObservation == null)
            {
                return NotFound();
            }
            return Ok(deletedBirdObservation);
        }


    }
}
