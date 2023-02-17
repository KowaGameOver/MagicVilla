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
        private readonly ApiResponse _response;
        private readonly ILogging _logger;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaAPIController(ILogging logger, IVillaRepository dbVilla, IMapper mapper)
        {
            _logger = logger;
            _dbVilla = dbVilla;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetVillasAsync()
        {
            try
            {
                _logger.Log("Get all villas", "");
                IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
                _response.Result = _mapper.Map<IEnumerable<VillaDTO>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<ActionResult<ApiResponse>> GetVillaAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.Log($"Id must be not null(id = {0})", "error");
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _dbVilla.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    _response.Result = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateVillaAsync([FromBody] VillaCreateDTO villaCreateDTO)
        {
            try
            {
                if (await _dbVilla.GetAsync(x => x.Name.ToLower() == villaCreateDTO.Name.ToLower()) != null)
                {
                    _logger.Log("Villa already Exist!","error");
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (villaCreateDTO == null)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Villa villa = _mapper.Map<Villa>(villaCreateDTO);

                await _dbVilla.CreateAsync(villa);
                _response.Result = villa;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteVillaAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.Result = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                var villa = await _dbVilla.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                await _dbVilla.RemoveAsync(villa);
                _response.Result = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateVillaAsync(int id, [FromBody] VillaUpdateDTO villaUpdateDTO)
        {
            try
            {
                if (id == 0 || villaUpdateDTO.Id != id)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Villa villa = _mapper.Map<Villa>(villaUpdateDTO);
                await _dbVilla.UpdateAsync(villa);
                _response.Result = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> UpdatePartialVillaAsync(int id, JsonPatchDocument<VillaUpdateDTO> patchVillaUpdateDTO)
        {
            try
            {
                if (patchVillaUpdateDTO == null || id == 0)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _dbVilla.GetAsync(v => v.Id == id, tracked: false);
                if (villa == null)
                {
                    _response.Result = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                VillaUpdateDTO villaUpdateDTO = _mapper.Map<VillaUpdateDTO>(villa);
                patchVillaUpdateDTO.ApplyTo(villaUpdateDTO, ModelState);
                villa = _mapper.Map<Villa>(villaUpdateDTO);
                if (!ModelState.IsValid)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                await _dbVilla.UpdateAsync(villa);
                _response.Result = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}



