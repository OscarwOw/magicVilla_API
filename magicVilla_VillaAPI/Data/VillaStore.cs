using magicVilla_VillaAPI.Models.Dto;

namespace magicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>() {
                new VillaDTO {ID=1,Name="chello"},
                new VillaDTO {ID=2,Name="Janko"}
            };
    }
}
