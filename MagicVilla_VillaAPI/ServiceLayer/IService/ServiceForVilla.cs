using AutoMapper;
using MagicVilla_VillaAPI.Errors;
using MagicVilla_VillaAPI.ExceptionFiltering;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.ServiceLayer.IService
{
    public class ServiceForVilla:Service<Villa>,IServiceForVilla
    {
        public async Task<VillaCreateDTO> OkCreateVillaAsyncResponse(VillaCreateDTO createDTO, IVillaRepository villaRepository, IMapper mapper)
        {
            if (createDTO == null)
            {
                throw new NullEntityException();
            }
            var created_villa = mapper.Map<Villa>(createDTO);
            await villaRepository.CreateAsync(created_villa);
            return createDTO;
        }

        public async Task<VillaDTO> OkDeleteVillaAsyncResponse(int id, IVillaRepository villaRepository)
        {
            if (id == 0)
            {
                throw new BadIdException();
            }
            var entity = await villaRepository.GetAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new NullEntityException();
            }
            await villaRepository.RemoveAsync(entity);
            return null;
        }

        public async Task<VillaDTO> OkGetVillaAsyncResponse(int id, IVillaRepository villaRepository, IMapper mapper)
        {
            if (id == 0)
            {
                throw new BadIdException();
            }
            var entity = await villaRepository.GetAsync(v => v.Id == id);
            var getDTO = mapper.Map<VillaDTO>(entity);
            return getDTO;
        }

        public async Task<VillaUpdateDTO> OkUpdateVillaAsyncResponse(int id, IVillaRepository villaRepository, IMapper mapper, VillaUpdateDTO updateDTO)
        {
            if (id == 0 || updateDTO.Id != id)
            {
                throw new BadIdException();
            }
            if (updateDTO == null)
            {
                throw new NullEntityException();
            }
            var updateVilla = mapper.Map<Villa>(updateDTO);
            await villaRepository.UpdateAsync(updateVilla);
            return updateDTO;
        }
    }
}
