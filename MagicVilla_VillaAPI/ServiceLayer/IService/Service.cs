using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Headers;

namespace MagicVilla_VillaAPI.ServiceLayer.IService
{
    public class Service<T>:IService<T> where T : class
    {
        public Task<List<T>> OkGetAllAsyncResponse(IRepository<T> repository, IMapper mapper)
        {
            var entitys = repository.GetAllAsync();
            if (entitys == null)
            {
                throw new Exception();
            }
            var responseDTO = mapper.Map<Task<List<T>>>(entitys);
            return responseDTO;
        }
    }
}
