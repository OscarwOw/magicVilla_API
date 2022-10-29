using magicVilla_VillaAPI.Data;
using magicVilla_VillaAPI.Models;
using magicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace magicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }
        [HttpGet("{id:int}",Name="GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.ID == id);
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
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO) 
        {
            if(villaDTO == null)
            {
                return BadRequest();
            }
            if (villaDTO.ID > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.ID= VillaStore.villaList.OrderByDescending(u => u.ID).FirstOrDefault().ID+1;
            VillaStore.villaList.Add(villaDTO);
            return CreatedAtRoute("GetVilla",new { id = villaDTO.ID},villaDTO);
        }
    }
}
