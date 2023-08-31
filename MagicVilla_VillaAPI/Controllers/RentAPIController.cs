using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.ExceptionFiltering;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper mapper;
        public RentAPIController(ApplicationDbContext db, IMapper _mapper)
        {
            _db = db;
            mapper = _mapper;
        }

        [HttpGet(Name = "GetRentalList")]
        public async Task<ActionResult<List<RentDTO>>> GetAllRentsAsync()
        {
            var get_rents = _db.Rents;
            var get_rents_dto = mapper.Map<List<RentDTO>>(get_rents);
            return Ok(get_rents_dto);
        }
        [HttpGet("GetRentByUnicCode", Name = "GetRentByUnicCode")]
        public async Task<ActionResult<Rent>> GetRentAsync(Guid unicCode)
        {
            if (_db.Rents.FirstOrDefault(uc => uc.UnicCode == unicCode) == null)
            {
                throw new Exception();
            }
            return Ok(_db.Rents.Where(ru => ru.UnicCode == unicCode));
        }
        [HttpPost("CreateRent", Name = "CreateRent")]
        public ActionResult<RentCreateDTO> CreateRentAsync(RentCreateDTO rentCreateDTO)
        {
            if (_db.Villas.FirstOrDefault(v => v.Id == rentCreateDTO.VillaId) == null)
            {
                throw new BadIdException();
            }
            if (_db.VillaNumbers.FirstOrDefault(vn => vn.VillaNo == rentCreateDTO.numberOfVilla) == null)
            {
                throw new BadIdException();
            }
            if (_db.Rents.FirstOrDefault(r=>r.numberOfVilla == rentCreateDTO.numberOfVilla && r.VillaId == rentCreateDTO.VillaId) != null)
            {
                throw new Exception();//Creating custom exception!!!
            }
            var new_rent = mapper.Map<Rent>(rentCreateDTO);
            new_rent.dateOfRegistry = DateTime.Now;
            new_rent.UnicCode = Guid.NewGuid();
            _db.Rents.Add(new_rent);
            _db.SaveChanges();

            return Ok(rentCreateDTO);
        }
        [HttpDelete]
        public async Task<ActionResult<Rent>> DeleteRentAsync(int id)
        {
            
            return Ok();
        }
    }
}
