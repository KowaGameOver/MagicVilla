using AutoMapper;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        private readonly ApiResponse _response;
        private readonly IVillaNumberRepository _dbVillaNumbers;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _dbVilla;
        public VillaNumberAPIController(IVillaNumberRepository dbVillaNumbers, IMapper mapper, IVillaRepository dbVilla)
        {
            _dbVillaNumbers = dbVillaNumbers;
            _mapper = mapper;
            _response = new();
            _dbVilla = dbVilla;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetVillaNumbersAsync()
        {
            try
            {
                IEnumerable<VillaNumber> villaListNumber = await _dbVillaNumbers.GetAllAsync();
                _response.Result = _mapper.Map<IEnumerable<VillaNumberDTO>>(villaListNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<ActionResult<ApiResponse>> GetVillaNumberAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villaNumber = await _dbVillaNumbers.GetAsync(v => v.VillaNo == id);
                if (villaNumber == null)
                {
                    _response.Result = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateVillaNumberAsync([FromBody] VillaNumberCreateDTO villaNumberCreateDTO)
        {
            try
            {
                if (await _dbVillaNumbers.GetAsync(x => x.VillaNo == villaNumberCreateDTO.VillaNo) != null)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (await _dbVilla.GetAsync(x => x.Id == villaNumberCreateDTO.VillaID) == null)
                {
                    ModelState.AddModelError("CustomEror", "Invalid id of Villa!");
                    return BadRequest(_response);
                }
                if (villaNumberCreateDTO == null)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                VillaNumber villaNumber = _mapper.Map<VillaNumber>(villaNumberCreateDTO);

                await _dbVillaNumbers.CreateAsync(villaNumber);
                _response.Result = villaNumber;
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteVillaNumberAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.Result = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                var villaNumber = await _dbVillaNumbers.GetAsync(v => v.VillaNo == id);
                if (villaNumber == null)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                await _dbVillaNumbers.RemoveAsync(villaNumber);
                _response.Result = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateVillaNumberAsync(int id, [FromBody] VillaNumberUpdateDTO villaNumberUpdateDTO)
        {
            try
            {
                if (id == 0 || villaNumberUpdateDTO.VillaNo != id)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (await _dbVilla.GetAsync(x => x.Id == villaNumberUpdateDTO.VillaID) == null)
                {
                    ModelState.AddModelError("CustomEror", "Invalid id of Villa!");
                    _response.ErrorMessages = new List<string>() { "Invalid id of Villa!" };
                    return BadRequest(_response);
                }
                VillaNumber villaNumber = _mapper.Map<VillaNumber>(villaNumberUpdateDTO);
                await _dbVillaNumbers.UpdateAsync(villaNumber);
                _response.Result = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> UpdatePartialVillaNumberAsync(int id, JsonPatchDocument<VillaNumberUpdateDTO> patchVillaNumberUpdateDTO)
        {
            try
            {
                if (patchVillaNumberUpdateDTO == null || id == 0)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _dbVillaNumbers.GetAsync(v => v.VillaNo == id, tracked: false);
                if (villa == null)
                {
                    _response.Result = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                VillaNumberUpdateDTO villaNumberUpdateDTO = _mapper.Map<VillaNumberUpdateDTO>(villa);
                patchVillaNumberUpdateDTO.ApplyTo(villaNumberUpdateDTO, ModelState);
                villa = _mapper.Map<VillaNumber>(villaNumberUpdateDTO);
                if (!ModelState.IsValid)
                {
                    _response.Result = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                await _dbVillaNumbers.UpdateAsync(villa);
                _response.Result = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
