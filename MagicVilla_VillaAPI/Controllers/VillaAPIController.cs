using AutoMapper;
using MagicVilla_VillaAPI.ExceptionFiltering;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using MagicVilla_VillaAPI.ServiceLayer.IService;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly IVillaRepository _villaRepository;
        private readonly IMapper _mapper;
        private readonly IServiceForVilla _serviceForVilla;
        public VillaAPIController(IVillaRepository villaRepository, IMapper mapper, IServiceForVilla serviceForVilla)
        {
            _villaRepository = villaRepository;
            _mapper = mapper;
            _serviceForVilla = serviceForVilla;
        }
        [HttpGet(Name = "GetVillas")]
        public async Task<ActionResult<List<VillaDTO>>> GetVillasAsync()
        {
            Console.WriteLine("Hello new branch");
            return Ok(await _serviceForVilla.OkGetAllAsyncResponse(_villaRepository, _mapper));
        }
        [HttpGet("GetVillaById/{id:int}", Name = "GetVillaById")]
        public async Task<ActionResult<VillaDTO>> GetVillaAsync(int id)
        {
            return Ok(await _serviceForVilla.OkGetVillaAsyncResponse(id, _villaRepository, _mapper));
        }
        [HttpPost("CreateVilla", Name = "CreateVilla")]
        public async Task<ActionResult<VillaCreateDTO>> CreateVillaAsync([FromBody] VillaCreateDTO createDTO)
        {
            return Ok(await _serviceForVilla.OkCreateVillaAsyncResponse(createDTO, _villaRepository, _mapper));
        }
        [HttpDelete("DeleteVillaById/{id:int}", Name = "DeleteVillaById")]
        public async Task<ActionResult<VillaDTO>> DeleteVillaAsync(int id)
        {
            return Ok(await _serviceForVilla.OkDeleteVillaAsyncResponse(id, _villaRepository));
        }
        [HttpPut("UpdateVillaById/{id:int}", Name = "UpdateVillaById")]
        public async Task<ActionResult<VillaUpdateDTO>> UpdateVillaAsync(int id, [FromBody] VillaUpdateDTO updateDTO)
        {
            return Ok(await _serviceForVilla.OkUpdateVillaAsyncResponse(id, _villaRepository, _mapper, updateDTO));
        }
    }
}



