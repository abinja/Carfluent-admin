using AutoMapper;
using GlobalEntityLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalEntityLayer.Models.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CarDetails,CarDetailsDTO>().ReverseMap();
            CreateMap<CarDetailsDTO,CarDetails>().ReverseMap();
        }
    }
}
