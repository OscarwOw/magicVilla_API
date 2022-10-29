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
        [HttpGet("{id:int}")]
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
    }
}
