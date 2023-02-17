using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            //CreateMap<VillaDTO, Villa>();
            CreateMap<Villa, VillaUpdateDTO>();
            CreateMap<VillaCreateDTO, Villa>();
            CreateMap<VillaUpdateDTO, Villa>();

            CreateMap<VillaNumber,VillaNumberDTO>();
            CreateMap<VillaNumberCreateDTO,VillaNumber>();
            CreateMap<VillaNumberUpdateDTO,VillaNumber>();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>();
        }
    }
}
