using magicVilla_VillaAPI.Data;
using magicVilla_VillaAPI.Models;
using magicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace magicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        private readonly ApplicationDatabaseContext _databaseContext;
        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationDatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("getting all villas");
            return Ok(_databaseContext.Villas);
        }
        [HttpGet("{Id:int}", Name = "GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> GetVilla(int Id)
        {
            if (Id == 0)
            {
                _logger.LogError("Get Villa Error with Id " + Id);
                return BadRequest();
            }
            
            var villa = _databaseContext.Villas.FirstOrDefault(u => u.Id == Id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);

        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            //if (!ModelState.IsValId)
            //{
            //    return BadRequest(ModelState);
            //}
            if (_databaseContext.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("customErr", "Villa already exists");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return BadRequest();
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.Id = _databaseContext.Villas.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            Villa model = new()
            {
                
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };
            _databaseContext.Villas.Add(model);
            _databaseContext.SaveChanges();
            return CreatedAtRoute("GetVilla", new { Id = villaDTO.Id }, villaDTO);
        }
        [HttpDelete("{Id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteVilla(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }
            var villa = _databaseContext.Villas.FirstOrDefault(u => u.Id == Id);
            if (villa == null)
            {
                return NotFound();
            }
            _databaseContext.Villas.Remove(villa);
            _databaseContext.SaveChanges();
            return NoContent();
        }
        [HttpPut("{Id:int}", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int Id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || Id!= villaDTO.Id)
            {
                return BadRequest();
            }
            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };
            _databaseContext.Villas.Update(model);
            _databaseContext.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{Id:int}", Name = "PartilUpdateVilla")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult PartialUpdateVilla(int Id,JsonPatchDocument<VillaDTO> patchDTO)
        {
            if(patchDTO==null || Id == 0)
            {
                return BadRequest();
            }
            var villa = _databaseContext.Villas.FirstOrDefault(u=> u.Id== Id);
            VillaDTO villaDTO = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };
            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };
            _databaseContext.Villas.Update(model);
            _databaseContext.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
