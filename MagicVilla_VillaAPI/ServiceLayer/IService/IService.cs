using AutoMapper;
using MagicVilla_VillaAPI.Repository.IRepository;

namespace MagicVilla_VillaAPI.ServiceLayer.IService
{
    public interface IService<T> where T : class
    {
        public Task<List<T>> OkGetAllAsyncResponse(IRepository<T> repository, IMapper mapper);
    }
}
