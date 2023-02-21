using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaAPIController(ILogging logger, IVillaRepository dbVilla, IMapper mapper)
        {
            _logger = logger;
            _dbVilla = dbVilla;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillasAsync()
        {
            _logger.Log("Get all villas", "");
            var villaList = await _dbVilla.GetAllAsync();
            var response = _mapper.Map<IEnumerable<VillaDTO>>(villaList);
            return Ok(response);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<ActionResult<VillaDTO>> GetVillaAsync(int id)
        {
            if (id == 0)
            {
                _logger.Log($"Id must be not null(id = {0})", "error");
                return BadRequest();
            }
            var villa = await _dbVilla.GetAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<VillaDTO>(villa);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaCreateDTO>> CreateVillaAsync([FromBody] VillaCreateDTO villaCreateDTO)
        {
            if (_dbVilla.FindByNameAsync(villaCreateDTO) == true)
            {
                return BadRequest();
            }
            if (villaCreateDTO == null)
            {
                return BadRequest();
            }
            var created_villa = _mapper.Map<Villa>(villaCreateDTO);

            await _dbVilla.CreateAsync(created_villa);
            
            return Ok(villaCreateDTO);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> DeleteVillaAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var villa = await _dbVilla.GetAsync(v => v.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            await _dbVilla.RemoveAsync(villa);
            var response = _mapper.Map<VillaDTO>(villa);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaUpdateDTO>> UpdateVillaAsync(int id, [FromBody] VillaUpdateDTO villaUpdateDTO)
        {
            if (id == 0 || villaUpdateDTO.Id != id)
            {
                return BadRequest();
            }
            var updated_villa = _mapper.Map<Villa>(villaUpdateDTO);
            await _dbVilla.UpdateAsync(updated_villa);

            return Ok(villaUpdateDTO);
        }

        //[HttpPatch]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<VillaUpdateDTO>> UpdatePartialVillaAsync(int id, JsonPatchDocument<VillaUpdateDTO> patchVillaUpdateDTO)
        //{
        //    if (patchVillaUpdateDTO == null || id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var villa = await _dbVilla.GetAsync(v => v.Id == id, tracked: false);
        //    if (villa == null)
        //    {
        //        return NotFound();
        //    }
        //    var villaUpdateDTO = _mapper.Map<VillaUpdateDTO>(villa);
        //    patchVillaUpdateDTO.ApplyTo(villaUpdateDTO, ModelState);
        //    villa = _mapper.Map<Villa>(villaUpdateDTO);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    await _dbVilla.UpdateAsync(villa);

        //    return Ok(villaUpdateDTO);
        //}
    }
}



