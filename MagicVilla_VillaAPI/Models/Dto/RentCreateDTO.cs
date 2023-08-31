namespace MagicVilla_VillaAPI.Models.Dto
{
    public class RentCreateDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int VillaId { get; set; }
        public int numberOfVilla { get; set; }
        public int DaysForRent { get; set; }

    }
}
