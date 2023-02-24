using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Net;

namespace MagicVilla_VillaAPI.ServiceLayer.IService
{
    public class ServiceForVilla:Service<Villa>,IServiceForVilla
    {
        public async Task<VillaCreateDTO> OkCreateVillaAsyncResponse(VillaCreateDTO createDTO, IVillaRepository villaRepository, IMapper mapper)
        {
            if (createDTO == null)
            {
                throw new Exception();
            }
            var created_villa = mapper.Map<Villa>(createDTO);
            await villaRepository.CreateAsync(created_villa);
            return createDTO;
        }

        public async Task<VillaDTO> OkDeleteVillaAsyncResponse(int id, IVillaRepository villaRepository)
        {
            if (id == 0)
            {
                throw new Exception();
            }
            var entity = await villaRepository.GetAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new Exception();
            }
            await villaRepository.RemoveAsync(entity);
            return null;
        }

        public async Task<VillaDTO> OkGetVillaAsyncResponse(int id, IVillaRepository villaRepository, IMapper mapper)
        {
            if (id == 0)
            {
                throw new Exception();
            }
            var entity = await villaRepository.GetAsync(v => v.Id == id);
            if (entity == null)
            {
                throw new Exception();
            }
            var getDTO = mapper.Map<VillaDTO>(entity);
            return getDTO;
        }

        public async Task<VillaUpdateDTO> OkUpdateVillaAsyncResponse(int id, IVillaRepository villaRepository, IMapper mapper, VillaUpdateDTO updateDTO)
        {
            if (id == 0)
            {
                throw new Exception();
            }
            if (updateDTO == null)
            {
                throw new Exception();
            }
            if (id != updateDTO.Id)
            {
                throw new Exception();
            }
            var updateVilla = mapper.Map<Villa>(updateDTO);
            await villaRepository.UpdateAsync(updateVilla);
            return updateDTO;
        }
    }
}
