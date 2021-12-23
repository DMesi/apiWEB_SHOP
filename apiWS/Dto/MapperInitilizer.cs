using apiWS.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiWS.Dto
{
    public class MapperInitilizer : Profile
    {


  //      Onda pozivamo u Startup.cs
  //      services.AddAutoMapper(typeof(MapperInitilizer));

        public MapperInitilizer()
        {
            CreateMap<Categories, CategoriesDTO>().ReverseMap();
            CreateMap<Locations, LocationsDTO>().ReverseMap();
            CreateMap<Products, ProductsDTO>().ReverseMap();
            CreateMap<Locations, LocationsCreateDTO>().ReverseMap();


           
        }
    }
}
