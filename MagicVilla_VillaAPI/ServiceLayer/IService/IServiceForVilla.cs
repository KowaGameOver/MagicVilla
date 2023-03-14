using AutoMapper;
using MagicVilla_VillaAPI.ExceptionFiltering;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.ServiceLayer.IService
{
    public interface IServiceForVilla:IService<Villa>
    {
        public Task<VillaDTO> OkGetVillaAsyncResponse(int id, IVillaRepository villaRepository, IMapper mapper);
        public Task<VillaCreateDTO> OkCreateVillaAsyncResponse(VillaCreateDTO createDTO, IVillaRepository villaRepository, IMapper mapper);
        public Task<VillaDTO> OkDeleteVillaAsyncResponse(int id, IVillaRepository villaRepository);
        public Task<VillaUpdateDTO> OkUpdateVillaAsyncResponse(int id, IVillaRepository villaRepository, IMapper mapper, VillaUpdateDTO updateDTO);

    }
}
