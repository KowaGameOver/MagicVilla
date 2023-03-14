using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.ServiceLayer.IService
{
    public interface IServiceForVillaNumber:IService<VillaNumber>
    {
        public Task<VillaNumberDTO> OkGetVillaNumberAsyncResponse(int id, IVillaNumberRepository villaNumberRepository, IMapper mapper);
        public Task<VillaNumberCreateDTO> OkCreateVillaNumberAsyncResponse(VillaNumberCreateDTO createDTO, IVillaNumberRepository villaNumberRepository,IVillaRepository villaRepository, IMapper mapper);
        public Task<VillaNumberDTO> OkDeleteVillaNumberAsyncResponse(int id,int villaId, IVillaNumberRepository villaNumberRepository);
        public Task<VillaNumberUpdateDTO> OkUpdateVillaNumberAsyncResponse(int id, IVillaNumberRepository villaNumberRepository,IVillaRepository villaRepository, IMapper mapper, VillaNumberUpdateDTO updateDTO);
    }
}
