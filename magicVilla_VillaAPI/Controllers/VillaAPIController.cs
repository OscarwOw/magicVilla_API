using magicVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace magicVilla_VillaAPI.Controllers
{
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        public IEnumerable<Villa> GetVillas()
        {
            return new List<Villa>
            {
                new Villa {ID=0,Name="chello"},
                new Villa {ID=0,Name="chello"}
            };
        }
    }
}
