using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class RentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int VillaId { get; set; }
        public int numberOfVilla { get; set; }
        public VillaNumber? villaNumber { get; set; }
        public DateTime dateOfRegistry { get; set; }
        public int DaysForRent { get; set; }
        public Guid UnicCode { get; set; }
    }
}
