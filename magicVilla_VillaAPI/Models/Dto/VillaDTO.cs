using System.ComponentModel.DataAnnotations;

namespace magicVilla_VillaAPI.Models.Dto
{
    public class VillaDTO
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
