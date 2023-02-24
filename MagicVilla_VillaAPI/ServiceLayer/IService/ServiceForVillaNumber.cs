using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.ServiceLayer.IService
{
    public class ServiceForVillaNumber : Service<VillaNumber>, IServiceForVillaNumber
    {
        public async Task<VillaNumberCreateDTO> OkCreateVillaNumberAsyncResponse(VillaNumberCreateDTO createDTO, IVillaNumberRepository villaNumberRepository,IVillaRepository villaRepository, IMapper mapper)
        {
            if (createDTO == null)
            {
                throw new Exception();
            }
            if (await villaRepository.GetAsync(x => x.Id == createDTO.VillaID) == null)
            {
                throw new Exception();
            }
            if (await villaNumberRepository.GetAsync(x => x.VillaNo == createDTO.VillaNo && x.VillaID == createDTO.VillaID) != null)
            {
                throw new Exception();
            }
            var villaNumber = mapper.Map<VillaNumber>(createDTO);
            await villaNumberRepository.CreateAsync(villaNumber);
            return createDTO;
        }

        public async Task<VillaNumberDTO> OkDeleteVillaNumberAsyncResponse(int VillaNo, IVillaNumberRepository villaNumberRepository)
        {
            if (VillaNo == 0)
            {
                throw new Exception();
            }
            var villaNumber = await villaNumberRepository.GetAsync(v => v.VillaNo == VillaNo);
            if (villaNumber == null)
            {
                throw new Exception();
            }
            await villaNumberRepository.RemoveAsync(villaNumber);
            return null;
        }

        public async Task<VillaNumberDTO> OkGetVillaNumberAsyncResponse(int VillaID, IVillaNumberRepository villaNumberRepository, IMapper mapper)
        {
            if (VillaID == 0)
            {
                throw new Exception();
            }
            var entity = await villaNumberRepository.GetAsync(v => v.VillaID == VillaID);
            if (entity == null)
            {
                throw new Exception();
            }
            var getDTO = mapper.Map<VillaNumberDTO>(entity);
            return getDTO;
        }

        public async Task<VillaNumberUpdateDTO> OkUpdateVillaNumberAsyncResponse(int VillaNo, IVillaNumberRepository villaNumberRepository,IVillaRepository villaRepository, IMapper mapper, VillaNumberUpdateDTO updateDTO)
        {
            if (VillaNo == 0 || updateDTO.VillaNo != VillaNo)
            {
                throw new Exception();
            }
            if (updateDTO == null)
            {
                throw new Exception();
            }
            if (await villaRepository.GetAsync(v => v.Id == updateDTO.VillaID) == null)
            {
                throw new Exception();
            }
            var villaNumber = mapper.Map<VillaNumber>(updateDTO);
            await villaNumberRepository.UpdateAsync(villaNumber);
            return updateDTO;
        }
    }
}
