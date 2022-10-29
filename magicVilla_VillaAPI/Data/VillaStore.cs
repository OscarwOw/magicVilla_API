using magicVilla_VillaAPI.Models.Dto;

namespace magicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>() {
                new VillaDTO {ID=1,Name="chello", Sqft = 100, Occupancy=4},
                new VillaDTO {ID=2,Name="Janko",Sqft=120, Occupancy=3}
            };
    }
}
