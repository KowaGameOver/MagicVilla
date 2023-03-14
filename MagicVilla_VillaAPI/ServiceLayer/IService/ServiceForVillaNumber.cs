using AutoMapper;
using MagicVilla_VillaAPI.Errors;
using MagicVilla_VillaAPI.ExceptionFiltering;
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
                throw new NullEntityException();
            }
            if (await villaRepository.GetAsync(v => v.Id == createDTO.VillaID) == null)
            {
                throw new NullEntityException();
            }
            if (await villaNumberRepository.GetAsync(vn => vn.VillaNo == createDTO.VillaNo && vn.VillaID == createDTO.VillaID) != null)
            {
                throw new EntityAlreadyExistException();
            }
            var villaNumber = mapper.Map<VillaNumber>(createDTO);
            await villaNumberRepository.CreateAsync(villaNumber);
            return createDTO;
        }

        public async Task<VillaNumberDTO> OkDeleteVillaNumberAsyncResponse(int VillaNo,int VillaID, IVillaNumberRepository villaNumberRepository)
        {
            if (VillaNo == 0)
            {
                throw new BadIdException();
            }
            if (VillaID == 0)
            {
                throw new BadIdException();
            }
            var villaNumber = await villaNumberRepository.GetAsync(vn => vn.VillaNo == VillaNo && vn.VillaID == VillaID);
            if (villaNumber == null)
            {
                throw new NullEntityException();
            }
            await villaNumberRepository.RemoveAsync(villaNumber);
            return null;
        }

        public async Task<VillaNumberDTO> OkGetVillaNumberAsyncResponse(int VillaID, IVillaNumberRepository villaNumberRepository, IMapper mapper)
        {
            if (VillaID == 0)
            {
                throw new BadIdException();
            }
            var entity = await villaNumberRepository.GetAsync(vn => vn.VillaID == VillaID);
            if (entity == null)
            {
                throw new NullEntityException();
            }
            var getDTO = mapper.Map<VillaNumberDTO>(entity);
            return getDTO;
        }

        public async Task<VillaNumberUpdateDTO> OkUpdateVillaNumberAsyncResponse(int VillaNo, IVillaNumberRepository villaNumberRepository,IVillaRepository villaRepository, IMapper mapper, VillaNumberUpdateDTO updateDTO)
        {
            if (VillaNo == 0 || updateDTO.VillaNo != VillaNo)
            {
                throw new BadIdException();
            }
            if (updateDTO == null)
            {
                throw new NullEntityException();
            }
            if (await villaRepository.GetAsync(v => v.Id == updateDTO.VillaID) == null)
            {
                throw new NullEntityException();
            }
            var villaNumber = mapper.Map<VillaNumber>(updateDTO);
            await villaNumberRepository.UpdateAsync(villaNumber);
            return updateDTO;
        }
    }
}
