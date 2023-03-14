using AutoMapper;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_VillaAPI.ServiceLayer.IService;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villaRepository;
        private readonly IServiceForVillaNumber _serviceForVillaNumber;

        public VillaNumberAPIController(IVillaNumberRepository villaNumberRepository, IMapper mapper, IVillaRepository villaRepository, IServiceForVillaNumber serviceForVillaNumber)
        {
            _villaNumberRepository = villaNumberRepository;
            _mapper = mapper;
            _villaRepository = villaRepository;
            _serviceForVillaNumber = serviceForVillaNumber;
        }

        [HttpGet("GetVillaNumbers",Name = "GetVillaNumbers")]
        public async Task<ActionResult<VillaNumberDTO>> GetVillaNumbersAsync()
        {
            return Ok(await _serviceForVillaNumber.OkGetAllAsyncResponse(_villaNumberRepository, _mapper));
        }

        [HttpGet("GetVillaNumberByVillaId/{VillaId:int}", Name = "GetVillaNumberByVillaId")]
        public async Task<ActionResult<VillaNumberDTO>> GetVillaNumberAsync(int VillaId)
        {
            return Ok(await _serviceForVillaNumber.OkGetVillaNumberAsyncResponse(VillaId, _villaNumberRepository, _mapper));
        }

        [HttpPost("CreateVillaNumber",Name = "CreateVillaNumber")]
        public async Task<ActionResult<VillaNumberCreateDTO>> CreateVillaNumberAsync([FromBody] VillaNumberCreateDTO createDTO)
        {
            return Ok(await _serviceForVillaNumber.OkCreateVillaNumberAsyncResponse(createDTO, _villaNumberRepository,_villaRepository,_mapper));
        }

        [HttpDelete("DeleteVillaNumberByVillaNumberAndVillaId/{VillaNo:int}", Name = "DeleteVillaNumberByVillaNumberAndVillaId")]
        public async Task<ActionResult<VillaNumberDTO>> DeleteVillaNumberAsync(int VillaNo,int VillaId)
        {
            return Ok(await _serviceForVillaNumber.OkDeleteVillaNumberAsyncResponse(VillaNo,VillaId, _villaNumberRepository));
        }

        [HttpPut("UpdateVillaNumberByVillaNumber/{VillaNo:int}", Name = "UpdateVillaNumberByVillaNumber")]
        public async Task<ActionResult<VillaNumberUpdateDTO>> UpdateVillaNumberAsync(int VillaNo, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            return Ok(await _serviceForVillaNumber.OkUpdateVillaNumberAsyncResponse(VillaNo, _villaNumberRepository,_villaRepository,_mapper,updateDTO));
        }
    }
}
