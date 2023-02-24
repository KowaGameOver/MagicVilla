using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace MagicVilla_VillaAPI.ServiceLayer.IService
{
    public interface IService<T> where T : class
    {
        public Task<List<T>> OkGetAllAsyncResponse(IRepository<T> repository, IMapper mapper);
    }
}
